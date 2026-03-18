using UnityEngine;

public class EnemyIdleState : IState<Enemy>
{
    public void OnEnter(Enemy ctx)
    {
       //Debug.Log("IdleEnter");

       ctx.NavMeshAgent.ResetPath();
    }

    public void OnUpdate(Enemy ctx)
    {
        if (ctx.LineOfSight.CanSeeTarget() || ctx.IsChaseRange())
        {
            ctx.SM.ChangeState(ctx.ChaseState);
        }  
    }

    public void OnExit(Enemy ctx)
    {
        //Debug.Log("IdleExit");
    }
}