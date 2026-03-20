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
        DropItem();
    }
    public void ApplyStun()
    {
        OnStun?.Invoke();
        DropItem();
    }

    public void DropItem()
    {
        ItemCanPickUp itemCanPickUp = GetComponentInChildren<ItemCanPickUp>();
        if (itemCanPickUp != null)
        {
            itemCanPickUp.gameObject.transform.parent = null;
            Collider itemCollider = itemCanPickUp.gameObject.GetComponent<Collider>();
            if (itemCollider != null) itemCollider.enabled = true;
            Rigidbody itemRb = itemCanPickUp.gameObject.GetComponent<Rigidbody>();
            if (itemRb != null)
            {
                itemRb.isKinematic = false;
                itemRb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
            }
        }
    }
}
