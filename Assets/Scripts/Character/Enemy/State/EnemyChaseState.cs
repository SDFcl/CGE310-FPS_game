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
