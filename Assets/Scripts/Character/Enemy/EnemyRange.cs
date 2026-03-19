using UnityEngine;

public class EnemyRange : Enemy
{   
    [Header("Attack")]
    [Tooltip("Enemy's Gun for do damage reference")]
    [SerializeField] private Gun gun;
    [SerializeField] private Transform shotingpoint;

    public Gun Gun => gun;

    protected override void ChangeAttackState()
    {
        AttackState = new EnemyRangeAttackState();
    }

    protected override void Awake()
    {
        base.Awake();
        if(gun == null || shotingpoint == null)
        {
            Debug.LogError("forgot to add reference in EnemyRange");
        }
    }

    protected override void Start()
    {
        base.Start();
        gun.SetShootPoint(shotingpoint);
        gun.SetAmmo(999999);
    }
}
