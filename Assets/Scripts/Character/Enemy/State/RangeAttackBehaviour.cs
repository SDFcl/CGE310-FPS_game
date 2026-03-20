using UnityEngine;

public class RangeAttackBehaviour : IEnemyAttack
{
    private readonly EnemyRange enemyRange;

    private bool attackStart;
    private bool attackEnded;

    private readonly float attackRange;
    private readonly float rotateSpeed;
    private readonly float facingThreshold;

    public RangeAttackBehaviour(
        EnemyRange enemyRange,
        float attackRange,
        float rotateSpeed = 90f,
        float facingThreshold = 0.0001f)
    {
        this.enemyRange = enemyRange;
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

        if (attackEnded && !ctx.IsAttackRange())
        {
            ctx.SM.ChangeState(ctx.ChaseState);
        }
    }

    public void OnExit(Enemy ctx)
    {
        ctx.NavMeshAgent.updateRotation = true;
        ctx.Animator.SetBool("CanAttack", false);
    }

    public void OnAttackHit(Enemy ctx)
    {
        enemyRange.Gun.Shoot();
    }

    public void OnAttackEnd(Enemy ctx)
    {
        ctx.Animator.SetBool("CanAttack", false);

        attackEnded = true;
        attackStart = false;
    }
}