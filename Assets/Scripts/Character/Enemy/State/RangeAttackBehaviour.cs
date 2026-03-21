using UnityEngine;

public class RangeAttackBehaviour : IEnemyAttack
{
    private readonly Gun gun;
    private readonly float attackRange;

    private bool attackStart;
    private bool attackEnded;

    public RangeAttackBehaviour(Gun gun, float attackRange)
    {
        this.gun = gun;
        this.attackRange = attackRange;
    }

    public float GetAttackRange() => attackRange;

    public void OnEnter(Enemy ctx)
    {
        attackStart = false;
        attackEnded = false;

        ctx.NavMeshAgent.ResetPath();
        ctx.NavMeshAgent.updateRotation = false;
    }

    public void OnUpdate(Enemy ctx)
    {
        if (!attackStart)
        {
            if (!ctx.IsFacingTarget(0.1f))
            {
                ctx.RotateToTarget(180f);
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
    }

    public void OnAttackHit(Enemy ctx)
    {
        if (gun != null)
            gun.Shoot();
    }

    public void OnAttackEnd(Enemy ctx)
    {
        attackEnded = true;
        attackStart = false;
    }
}