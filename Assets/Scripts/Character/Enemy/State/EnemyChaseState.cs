using UnityEngine;

public class EnemyChaseState : IState<Enemy>
{
    private float timer;
    public void OnEnter(Enemy ctx)
    {
       //Debug.Log("ChaseEnter");
    }

    public void OnUpdate(Enemy ctx)
    {
        bool isMoving = ctx.NavMeshAgent.velocity.sqrMagnitude > 0.01f;
        ctx.Animator.SetBool("isMoving", isMoving);
        
        timer += Time.deltaTime;
        if (timer >= ctx.UpdateRate)
        {
            timer = 0f;
            ctx.NavMeshAgent.SetDestination(ctx.LineOfSight.Target.position);
        }

        if (ctx.IsAttackRange())
        {
            ctx.SM.ChangeState(ctx.AttackState);
        }
        else
        {
            ctx.Animator.SetBool("CanAttack",false);
        }

        if (ctx.IsIdleRange() && !ctx.LineOfSight.CanSeeTarget())
        {
            ctx.SM.ChangeState(ctx.IdleState);
        }
    }

    public void OnExit(Enemy ctx)
    {
        //Debug.Log("ChaseExit");
    }
}
