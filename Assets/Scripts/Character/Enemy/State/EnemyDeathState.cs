using UnityEngine;

public class EnemyDeathState : IState<Enemy>
{
    public void OnEnter(Enemy ctx)
    {
       Debug.Log("DeathEnter");

       ctx.NavMeshAgent.ResetPath();
       ctx.Animator.SetTrigger("Death");
    }

    public void OnUpdate(Enemy ctx)
    {
        
    }

    public void OnExit(Enemy ctx)
    {
        Debug.Log("DeathExit");
    }

    public void OnDie(Enemy ctx)
    {
        Debug.Log("OnDie");

        ctx.gameObject.SetActive(false);
    }
}
