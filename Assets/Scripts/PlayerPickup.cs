using System;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private bool _isPickup = false;

    public GameObject itemHolder;
    public Transform itemHoldPoint;

    public Action OnPickup;

    public bool setItem(GameObject item)
    {
        if (!_isPickup)
        {
            _isPickup = true;

            itemHolder = item;
            itemHolder.transform.SetParent(itemHoldPoint);
            itemHolder.transform.localPosition = Vector3.zero;
            itemHolder.transform.localRotation = Quaternion.identity;
            itemHolder.transform.localScale = Vector3.one;
            itemHolder.layer = LayerMask.NameToLayer("Default");
            Collider collider = itemHolder.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
            Rigidbody rb = itemHolder.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                /*rb.velocity = Vector3.zero; //Code by Copilot
                rb.angularVelocity = Vector3.zero;*/
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            OnPickup?.Invoke();

            return true;
        }
        return false;
    }

    public GameObject TakeItem()
    {
        if (!_isPickup) return null;

        GameObject item = itemHolder;

        itemHolder = null;
        _isPickup = false;

        return item;
    }
}