using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public StateMachine<Enemy> SM { get; private set; }

    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyStunState StunState { get; private set; }
    public EnemyDeathState DeathState { get; private set; }

    public IEnemyAttack AttackBehaviour { get; protected set; }

    public LineOfSight LineOfSight { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public Animator Animator { get; private set; }
    public float UpdateRate => updateRate;
    public float LifeTimeBody => lifeTimeBody;

    [SerializeField] bool showGizmos = true;

    [Header("Idle")]
    [SerializeField] float forceChaseRange = 3f;

    [Header("Chase")]
    [SerializeField] float updateRate = 0.1f;
    [SerializeField] float idleRange = 10f;

    [Header("Death")]
    [SerializeField] float lifeTimeBody = 20f;

    [Header("Fallback Fist Attack")]
    [SerializeField] private Collider fistHitbox;
    [SerializeField] private float fistAttackRange = 1.5f;
    private float fistRotateSpeed = 360f;
    private float fistFacingThreshold = 5f;

    [Header("Comic Effect")]
    [SerializeField] private GameObject comicEffect;

    public Collider FistHitbox => fistHitbox;
    public float FistAttackRange => fistAttackRange;

    private EnemyHitHandle hitHandle;

    protected virtual void Awake()
    {
        SM = new StateMachine<Enemy>(this);

        IdleState = new EnemyIdleState();
        ChaseState = new EnemyChaseState();
        AttackState = new EnemyAttackState();
        StunState = new EnemyStunState();
        DeathState = new EnemyDeathState();

        LineOfSight = GetComponent<LineOfSight>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        hitHandle = GetComponent<EnemyHitHandle>();

        comicEffect.SetActive(false);

        ConfigureAttackBehaviour();
    }

    protected virtual void Start()
    {
        hitHandle.OnStun += (bool canDrop) => StunEnemy();
        hitHandle.HealthSystem.OnDied += EnemyDie;

        SM.Initialize(IdleState);
    }

    void OnDisable()
    {
        hitHandle.OnStun -= (bool canDrop) => StunEnemy();
        hitHandle.HealthSystem.OnDied -= EnemyDie;
    }

    protected virtual void Update()
    {
        SM.Tick();
    }

    protected virtual void ConfigureAttackBehaviour()
    {
    }

    public bool IsChaseRange()
    {
        float distance = Vector3.Distance(transform.position, LineOfSight.Target.position);
        return distance <= forceChaseRange;
    }

    public bool IsAttackRange()
    {
        if (AttackBehaviour == null) return false;

        float distance = Vector3.Distance(transform.position, LineOfSight.Target.position);
        return distance <= AttackBehaviour.GetAttackRange();
    }

    public bool IsIdleRange()
    {
        float distance = Vector3.Distance(transform.position, LineOfSight.Target.position);
        return distance >= idleRange;
    }

    public void StunEnemy()
    {
        if (hitHandle.HealthSystem.CurrentHP > 0)
            SM.ChangeState(StunState);
            comicEffect.SetActive(true);
    }

    public void EnemyDie()
    {
        SM.ChangeState(DeathState);
        comicEffect.SetActive(false);
    }

    public void Animation_AttackHit()
    {
        if (SM.CurrentState == AttackState)
        {
            AttackBehaviour?.OnAttackHit(this);
        }
    }

    public void Animation_AttackEnd()
    {
        if (SM.CurrentState == AttackState)
        {
            AttackBehaviour?.OnAttackEnd(this);
        }
    }

    public void Animation_StunEnd()
    {
        if (SM.CurrentState == StunState)
        {
            StunState.OnStunEnd(this);
            comicEffect.SetActive(false);
        }
    }

    public void RotateToTarget(float speedRotate = 360f)
    {
        Vector3 dir = LineOfSight.Target.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.001f)
            return;

        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, speedRotate * Time.deltaTime);
    }

    public bool IsFacingTarget(float angleThreshold = 5f)
    {
        Vector3 dir = LineOfSight.Target.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.001f) return true;

        float angle = Vector3.Angle(transform.forward, dir.normalized);
        return angle <= angleThreshold;
    }

    public void SetAttackBehaviour(IEnemyAttack newBehaviour)
    {
        if (newBehaviour == null)
        {
            Debug.LogError($"{name} tried to set null AttackBehaviour");
            return;
        }

        AttackBehaviour?.OnExit(this);
        AttackBehaviour = newBehaviour;

        if (SM != null && SM.CurrentState == AttackState)
        {
            AttackBehaviour.OnEnter(this);
        }
    }
    public void UseFistAttack()
    {
        if (fistHitbox == null)
        {
            Debug.LogWarning($"{name} has no fistHitbox assigned.");
            return;
        }

        Animator.SetBool("HasWeapon", false);

        SetAttackBehaviour(
            new MeleeAttackBehaviour(
                fistHitbox,
                fistAttackRange,
                fistRotateSpeed,
                fistFacingThreshold
            )
        );
    }

    protected virtual void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, forceChaseRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, idleRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, fistAttackRange);
    }
}