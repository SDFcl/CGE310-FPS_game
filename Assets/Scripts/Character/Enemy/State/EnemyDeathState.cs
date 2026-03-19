using Unity.VisualScripting;
using UnityEngine;

public class EnemyDeathState : IState<Enemy>
{
    public void OnEnter(Enemy ctx)
    {
       //Debug.Log("DeathEnter");

       ctx.NavMeshAgent.ResetPath();

       ctx.NavMeshAgent.enabled = false;
       ctx.GetComponent<Collider>().enabled = false;
       ctx.Animator.enabled = false;
       ctx.GetComponentInChildren<RagdollController>().ragdollEnable = true;
       Object.Destroy(ctx.gameObject,ctx.LifeTimeBody);
    }

    public void OnUpdate(Enemy ctx)
    {
        
    }

    public void OnExit(Enemy ctx)
    {
        //Debug.Log("DeathExit");
    }
}
