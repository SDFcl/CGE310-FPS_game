public class EnemyAttackState : IState<Enemy>
{
    public void OnEnter(Enemy ctx)
    {
        ctx.AttackBehaviour?.OnEnter(ctx);
    }

    public void OnUpdate(Enemy ctx)
    {
        ctx.AttackBehaviour?.OnUpdate(ctx);
    }

    public void OnExit(Enemy ctx)
    {
        ctx.AttackBehaviour?.OnExit(ctx);
    }
}