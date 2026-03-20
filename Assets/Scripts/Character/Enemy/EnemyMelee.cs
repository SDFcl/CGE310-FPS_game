using UnityEngine;

public class EnemyMelee : Enemy
{
    [Header("Weapon Attack")]
    [SerializeField] private Collider weaponHitbox;
    [SerializeField] private float weaponAttackRange = 2.5f;

    [Header("State")]
    [SerializeField] private bool hasWeapon = true;

    public bool HasWeapon => hasWeapon;
    public Collider WeaponHitbox => weaponHitbox;

    protected override void Start()
    {
        base.Start();
        Animator.SetBool("HasWeapon", hasWeapon);
    }

    protected override void ConfigureAttackBehaviour()
    {
        if (hasWeapon)
        {
            AttackBehaviour = new MeleeAttackBehaviour(weaponHitbox, weaponAttackRange);
        }
        else
        {
            UseFistAttack();
        }
    }

    public void DropWeapon()
    {
        if (!hasWeapon) return;

        hasWeapon = false;

        if (weaponHitbox != null)
            weaponHitbox.gameObject.SetActive(false);

        UseFistAttack();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DropWeapon();
        }
    }
}