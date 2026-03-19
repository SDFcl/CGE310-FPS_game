using UnityEngine;

public class PlayerDamageHandle : MonoBehaviour,IDamageable
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHP;
    public HealthSystem Health{get; private set;}
    void Awake()
    {
        Health = new(maxHealth);
    }
    void OnEnable()
    {
        Health.OnDied += PlayerDie;
        Health.OnHealthChanged += PlayerHPChange;
    }
    void ODisable()
    {
        Health.OnDied -= PlayerDie;
        Health.OnHealthChanged -= PlayerHPChange;
    }
    void Update()
    {
        //Debug only
        currentHP = Health.CurrentHP;
    }
    public void TakeDamage(float damage)
    {
        //Take Damage Here
        if(Health.IsDead) return;
        Debug.Log($"palyer take damage = {damage}");
        Health.TakeDamage(damage);
    }

    public void PlayerDie()
    {
        // Player Die Logic Here
        Debug.Log("Player is Die");
    }

    public void PlayerHPChange(float currentHP)
    {
        // When ChangeHP Play Logic Here
        Debug.Log($"CurrentHp is {currentHP}");
    }

}
