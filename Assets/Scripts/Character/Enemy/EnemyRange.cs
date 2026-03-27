using UnityEngine;

public class EnemyRange : Enemy
{
    [Header("Attack")]
    [SerializeField] private Gun gun;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float attackRange = 8f;

    [Header("State")]
    [SerializeField] private bool hasWeapon = true;

    public Gun Gun => gun;
    public bool HasWeapon => hasWeapon;

    //observerPattern
    private EnemyHitHandle enemyHit;

    protected override void Awake()
    {
        base.Awake();

        if (gun == null || shootingPoint == null)
        {
            Debug.LogError("Forgot to assign gun or shootingPoint in EnemyRange");
        }
        enemyHit = GetComponent<EnemyHitHandle>();
    }
    void OnDisable()
    {
        enemyHit.OnStun -= DropWeapon;
        enemyHit.HealthSystem.OnDied -= () => DropWeapon(true);
    }

    protected override void Start()
    {
        base.Start();

        if (gun == null || shootingPoint == null)
            return;

        enemyHit.OnStun += DropWeapon;
        enemyHit.HealthSystem.OnDied += () => DropWeapon(true);

        gun.SetShootPoint(shootingPoint);
        gun.SetAmmo(999999);

        Animator.SetBool("HasWeapon", hasWeapon);
    }

    protected override void ConfigureAttackBehaviour()
    {
        if (hasWeapon)
        {
            AttackBehaviour = new RangeAttackBehaviour(gun, attackRange);
        }
        else
        {
            UseFistAttack(); // ใช้ของกลางจาก Enemy
        }
    }

    public void DropWeapon(bool canDrop)
    {
        if (!canDrop) return;
        if (!hasWeapon) return;

        hasWeapon = false;

        gun.SetAmmo(gun.AmmoAmount);
        gun.SetShootPoint(null);
        gun = null;
        UseFistAttack();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}