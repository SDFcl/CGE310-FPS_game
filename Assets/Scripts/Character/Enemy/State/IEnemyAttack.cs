public interface IEnemyAttack
{
    void OnEnter(Enemy enemy);
    void OnUpdate(Enemy enemy);
    void OnExit(Enemy enemy);

    void OnAttackHit(Enemy enemy);
    void OnAttackEnd(Enemy enemy);

    float GetAttackRange();
}