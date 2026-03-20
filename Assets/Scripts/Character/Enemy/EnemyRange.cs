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

    protected override void Awake()
    {
        base.Awake();

        if (gun == null || shootingPoint == null)
        {
            Debug.LogError("Forgot to assign gun or shootingPoint in EnemyRange");
        }
    }

    protected override void Start()
    {
        base.Start();

        if (gun == null || shootingPoint == null)
            return;

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

    public void DropWeapon()
    {
        if (!hasWeapon) return;

        hasWeapon = false;

        if (gun != null)
            gun.gameObject.transform.parent = null;

        UseFistAttack();
    }

    // test
    /*protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DropWeapon();
        }
    }*/
}