using UnityEngine;

public class EnemyMelee : Enemy
{
    [Header("Attack")]
    [SerializeField] private Collider hitbox;
    [SerializeField] private float attackRange = 2.5f;

    public Collider Hitbox => hitbox;

    protected override void ConfigureAttackBehaviour()
    {
        AttackBehaviour = new MeleeAttackBehaviour(this, attackRange);
    }
}