using UnityEngine;

public class EnemyStunState : IState<Enemy>
{
    public void OnEnter(Enemy ctx)
    {
       Debug.Log("StunEnter");

       ctx.NavMeshAgent.ResetPath();
       ctx.Animator.SetTrigger("Stun");
    }

    public void OnUpdate(Enemy ctx)
    {
        
    }

    public void OnExit(Enemy ctx)
    {
        Debug.Log("StunExit");
    }

    public void OnStunEnd(Enemy ctx)
    {
        Debug.Log("Stun End");

        ctx.SM.ChangeState(ctx.ChaseState);
    }
}
