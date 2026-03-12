using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour,IPooledObject
{
    [SerializeField] float speed = 40f;
    [SerializeField] float lifeTime = 20f;
    Rigidbody rb;
    float damage;
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
        Debug.Log("Hit damage = " + damage);
        gameObject.SetActive(false);
    }
    public void SetDamage(float value)
    {
        damage = value;
    }
}
