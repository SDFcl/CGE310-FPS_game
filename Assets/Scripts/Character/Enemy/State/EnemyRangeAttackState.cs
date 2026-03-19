using UnityEngine;

public class EnemyRangeAttackState : EnemyAttackStateBase
{
    private bool attackStart;
    private bool attackEnded;
    EnemyRange enemyRange;
    public override void OnEnter(Enemy ctx)
    {
    Debug.Log("AttackEnter");
       enemyRange = ctx as EnemyRange;

       if (enemyRange == null)
        {
            Debug.LogError("EnemyMeleeAttackState requires EnemyMelee context.");
            return;
        }

       attackStart = false;
       attackEnded = false;

       ctx.NavMeshAgent.ResetPath();
       ctx.NavMeshAgent.updateRotation = false;
    }

    public override void OnUpdate(Enemy ctx)
    {
        if (!attackStart)
        {
            if (!ctx.IsFacingTarget(0.0001f))
            {
                Debug.Log("Roatating");
                ctx.RotateToTarget(90f);
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

    public override void OnExit(Enemy ctx)
    {
        Debug.Log("AttackExit");
        ctx.NavMeshAgent.updateRotation = true;
    }

    //Unity Animation Event
    public override void OnAttackHit(Enemy ctx)
    {
        Debug.Log("Attack Hit");
        enemyRange.Gun.Shoot();
    }

    public override void OnAttackEnd(Enemy ctx)
    {
        Debug.Log("Attack End");
        attackEnded = true;
        attackStart = false;
    }
}