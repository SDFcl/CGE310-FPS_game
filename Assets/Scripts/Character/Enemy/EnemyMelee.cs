using UnityEngine;

public class EnemyMelee : Enemy
{   
    [Header("Attack")]
    [Tooltip("Enemy's Hixbox for do damage reference")]
    [SerializeField] Collider hitbox;

    public Collider Hitbox => hitbox;

    protected override void ChangeAttackState()
    {
        AttackState = new EnemyMeleeAttackState();
    }
    
}
