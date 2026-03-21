using UnityEngine;

public class MeleeAttackBehaviour : IEnemyAttack
{
    private readonly Collider hitbox;

    private bool attackStart;
    private bool attackEnded;

    private readonly float attackRange;
    private readonly float rotateSpeed;
    private readonly float facingThreshold;

    public MeleeAttackBehaviour(
        Collider hitbox,
        float attackRange,
        float rotateSpeed = 360f,
        float facingThreshold = 5f)
    {
        this.hitbox = hitbox;
        this.attackRange = attackRange;
        this.rotateSpeed = rotateSpeed;
        this.facingThreshold = facingThreshold;
    }

    public float GetAttackRange() => attackRange;

    public void OnEnter(Enemy ctx)
    {
        attackStart = false;
        attackEnded = false;

        ctx.NavMeshAgent.ResetPath();
        ctx.NavMeshAgent.updateRotation = false;

        if (hitbox != null)
            hitbox.enabled = false;
    }

    public void OnUpdate(Enemy ctx)
    {
        if (!attackStart)
        {
            if (!ctx.IsFacingTarget(facingThreshold))
            {
                ctx.RotateToTarget(rotateSpeed);
            }
            else
            {
                attackStart = true;
                ctx.Animator.SetBool("CanAttack", true);
            }
        }

        if (!ctx.IsAttackRange() && attackEnded)
        {
            ctx.SM.ChangeState(ctx.ChaseState);
        }
    }

    public void OnExit(Enemy ctx)
    {
        ctx.NavMeshAgent.updateRotation = true;

        if (hitbox != null)
            hitbox.enabled = false;
    }

    public void OnAttackHit(Enemy ctx)
    {
        if (hitbox != null)
            hitbox.enabled = true;
    }

    public void OnAttackEnd(Enemy ctx)
    {
        if (hitbox != null)
            hitbox.enabled = false;

        attackEnded = true;
        attackStart = false;
    }
}