using UnityEngine;

public class EnemyMelee : Enemy
{   public Collider Hitbox => hitbox;

    [Header("Attack")]
    [Tooltip("Enemy's Hixbox for do damage reference")]
    [SerializeField] Collider hitbox;

    protected override void ChangeAttackState()
    {
        AttackState = new EnemyMeleeAttackState();
    }
    
}
