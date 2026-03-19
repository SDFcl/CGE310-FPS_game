using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IPooledObject
{
    [SerializeField] float speed = 40f;
    [SerializeField] float lifeTime = 20f;
    Rigidbody rb;
    float damage;
    private GameObject owner;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnObjectSpawn()
    {
        rb.linearVelocity = transform.forward * speed;
        StartCoroutine(Disabler());
    }
    private IEnumerator Disabler()
	{
		yield return new WaitForSeconds(lifeTime);
		gameObject.SetActive(false);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == owner)
        {
            gameObject.SetActive(false);
            return;
        }

        if (owner != null && collision.gameObject.CompareTag(owner.tag))
        {
            gameObject.SetActive(false);
            return;
        }
        
        if(collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
        gameObject.SetActive(false);
    }
    public void SetDamage(float value)
    {
        damage = value;
    }
    public void SetOwner(GameObject shooter)
    {
        owner = shooter;
    }
}
