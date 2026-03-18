public abstract class EnemyAttackStateBase : IState<Enemy>
{
    public virtual void OnEnter(Enemy context) { }
    public virtual void OnUpdate(Enemy context) { }
    public virtual void OnExit(Enemy context) { }

    public virtual void OnAttackHit(Enemy enemy) { }
    public virtual void OnAttackEnd(Enemy enemy) { }
}