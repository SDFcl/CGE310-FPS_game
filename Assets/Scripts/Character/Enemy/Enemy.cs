using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //State Pattern
    public StateMachine<Enemy> SM { get; private set; }

    public EnemyIdleState IdleState {get; private set;}
    public EnemyChaseState ChaseState {get; private set;}
    public EnemyAttackStateBase AttackState {get; protected set;}
    public EnemyStunState StunState {get; private set;}
    public EnemyDeathState DeathState {get; private set;}

    //Ref
    public LineOfSight LineOfSight {get; private set;}
    public NavMeshAgent NavMeshAgent {get; private set;}
    public Animator Animator{get; private set;}
    public float UpdateRate => updateRate;
    public float LifeTimeBody => lifeTimeBody;
    

    [TextArea]
    [SerializeField] string gizmosDescription;
    [SerializeField] bool showGizmos = true;

    [Header("Idle")]
    [Tooltip("Range to force to enter Chasestate")]
    [SerializeField] float ForcechaseRange = 3f;

    [Header("Chase")]
    [Tooltip("What time to update Nevmesh Agent")]
    [SerializeField] float updateRate = 0.1f;
    [Tooltip("Range to enter Attackstate")]
    [SerializeField] float attackRange = 2.5f;
    [Tooltip("If Target out of Range enter IdleState")]
    [SerializeField] float idleRange = 10f;

    [Header("Death")]
    [SerializeField] float lifeTimeBody = 20f;
    

    //ObserverPattern
    private EnemyHitHandle hitHandle;

    
    void Awake()
    {
        //Create Reference for StatePattern
        SM = new StateMachine<Enemy>(this);

        IdleState = new EnemyIdleState();
        ChaseState = new EnemyChaseState();
        ChangeAttackState();
        StunState = new EnemyStunState();
        DeathState = new EnemyDeathState();

        //Create Reference
        LineOfSight = GetComponent<LineOfSight>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();

        //ObserverPattern
        hitHandle = GetComponent<EnemyHitHandle>();
    }
    void OnDisable()
    {
        hitHandle.OnStun -= StunEnemy;
        hitHandle.HealthSystem.OnDied -= EnemyDie;
    }

    void Start()
    {
        hitHandle.OnStun += StunEnemy;
        hitHandle.HealthSystem.OnDied += EnemyDie;

        SM.Initialize(IdleState);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            
        }
        SM.Tick();
    }

    protected virtual void ChangeAttackState()
    {
        
    }

    //Funtion use by concrete
    public bool IsChaseRange()
    {
        float distance = Vector3.Distance(transform.position, LineOfSight.Target.position);
        return distance <= ForcechaseRange;
    }
    public bool IsAttackRange()
    {
        float distance = Vector3.Distance(transform.position, LineOfSight.Target.position);
        return distance <= attackRange;
    }
    public bool IsIdleRange()
    {
        float distance = Vector3.Distance(transform.position, LineOfSight.Target.position);
        return distance >= idleRange;
    }

    // API Funtion
    public void StunEnemy()
    {
        if(hitHandle.HealthSystem.CurrentHP > 0)
            SM.ChangeState(StunState);
    }

    public void EnemyDie()
    {
        SM.ChangeState(DeathState);
    }

    // Unity Animation Event
    public void Animation_AttackHit()
    {
        if (SM.CurrentState == AttackState)
        {
            AttackState.OnAttackHit(this);
        }
    }

    public void Animation_AttackEnd()
    {
        if (SM.CurrentState == AttackState)
        {
            AttackState.OnAttackEnd(this);
        }
    }

    public void Animation_StunEnd()
    {
        if (SM.CurrentState == StunState)
        {
            StunState.OnStunEnd(this);
        }
    }

    //RotateEnemy
    public void RotateToTarget()
    {
        Vector3 dir = LineOfSight.Target.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.001f)
            return;

        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRot, 90f * Time.deltaTime);
    }

    public bool IsFacingTarget(float angleThreshold = 5f)
    {
        Vector3 dir = LineOfSight.Target.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.001f) return true;

        float angle = Vector3.Angle(transform.forward, dir.normalized);
        return angle <= angleThreshold;
    }

    //Debug
    void OnDrawGizmosSelected()
    {
        if(!showGizmos)
        return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ForcechaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, idleRange);
    }
}
