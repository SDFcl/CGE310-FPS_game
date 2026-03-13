using System;
using UnityEngine;

public class PlayerThrowItem : MonoBehaviour
{
    public PlayerPickup showItem;
    public float throwForce = 10f;

    public Camera cam;
    public Action OnThrow;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // �Ҵ��»��� G
        {
            Throw();
        }
    }

    void Throw()
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

    }
}