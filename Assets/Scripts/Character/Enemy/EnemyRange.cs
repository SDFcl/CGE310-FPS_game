using UnityEngine;

public class EnemyRange : Enemy
{
    [Header("Attack")]
    [SerializeField] private Gun gun;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float attackRange = 8f;

    public Gun Gun => gun;

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

        gun.SetShootPoint(shootingPoint);
        gun.SetAmmo(999999);
    }

    protected override void ConfigureAttackBehaviour()
    {
        AttackBehaviour = new RangeAttackBehaviour(this, attackRange);
    }
}