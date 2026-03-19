using UnityEngine;

public class EnemyMeleeAttackState : EnemyAttackStateBase
{
    private bool attackStart;
    private bool attackEnded;
    EnemyMelee enemyMelee;
    public override void OnEnter(Enemy ctx)
    {
       //Debug.Log("AttackEnter");
       enemyMelee = ctx as EnemyMelee;
       if (enemyMelee == null)
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
            if (!ctx.IsFacingTarget())
            {
                ctx.RotateToTarget();
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
        //Debug.Log("AttackExit");
        ctx.NavMeshAgent.updateRotation = true;
        enemyMelee.Hitbox.enabled = false;
    }

    //Unity Animation Event
    public override void OnAttackHit(Enemy ctx)
    {
        //Debug.Log("Attack Hit");

        enemyMelee.Hitbox.enabled = true;
    }

    public override void OnAttackEnd(Enemy ctx)
    {
        //Debug.Log("Attack End");

        enemyMelee.Hitbox.enabled = false;
        

        attackEnded = true;
    }
}
