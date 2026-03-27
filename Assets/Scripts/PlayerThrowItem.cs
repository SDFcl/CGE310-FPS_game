using System;
using UnityEngine;

public class PlayerThrowItem : MonoBehaviour
{
    public PlayerPickup showItem;
    public float throwForce = 10f;

  
    public Transform throwPoint;
    public Action OnThrow;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // �Ҵ��»��� G
        {
            Throw();
        }
    }

    /*void Throw()
    {
        GameObject item = showItem.TakeItem();

        if (item == null) return;

        item.transform.SetParent(null);

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody>();
        }

        rb.isKinematic = false;
        rb.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);
        OnThrow?.Invoke();

    }*/

    public void Throw()
    {
        GameObject item = showItem.TakeItem();
        if (item == null) return;

        ItemCanPickUp itemCanPickUp = item.GetComponent<ItemCanPickUp>();
        Destroy(item);
         Animator anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("Throw");
         GameObject obj = Instantiate(itemCanPickUp.throwablePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        OnThrow?.Invoke();
    }
}