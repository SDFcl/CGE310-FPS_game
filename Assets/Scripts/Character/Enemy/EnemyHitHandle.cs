using System;
using UnityEngine;

public class EnemyHitHandle : MonoBehaviour,IDamageable,IStunable
{
    public HealthSystem HealthSystem {get; private set;}
    public Action OnStun;
    void Awake()
    {
        HealthSystem = new(1f);
    }
    public void TakeDamage(float _)
    {
        HealthSystem.Kill();
    }
    public void ApplyStun()
    {
        OnStun?.Invoke();
        // Drop Item
    }
}
