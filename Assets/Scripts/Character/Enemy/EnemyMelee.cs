using UnityEngine;

public class EnemyMelee : Enemy
{
    [Header("Weapon Attack")]
    [SerializeField] private Collider weaponHitbox;
    [SerializeField] private float attackRange = 2.5f;

    [Header("State")]
    [SerializeField] private bool hasWeapon = true;

    public bool HasWeapon => hasWeapon;
    public Collider WeaponHitbox => weaponHitbox;

    //observerPattern
    private EnemyHitHandle enemyHit;
    protected override void Awake()
    {
        base.Awake();
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
        
        enemyHit.OnStun += DropWeapon;
        enemyHit.HealthSystem.OnDied += () => DropWeapon(true);

        Animator.SetBool("HasWeapon", hasWeapon);
    }

    protected override void ConfigureAttackBehaviour()
    {
        if (hasWeapon)
        {
            AttackBehaviour = new MeleeAttackBehaviour(weaponHitbox, attackRange);
        }
        else
        {
            UseFistAttack();
        }
    }

    public void DropWeapon(bool canDrop)
    {
        if(!canDrop) return;
        if (!hasWeapon) return;

        hasWeapon = false;

        if (weaponHitbox != null)
            weaponHitbox.gameObject.SetActive(false);

        UseFistAttack();
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}