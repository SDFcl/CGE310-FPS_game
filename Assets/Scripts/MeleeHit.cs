using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    [SerializeField] float damage;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
    }
}
