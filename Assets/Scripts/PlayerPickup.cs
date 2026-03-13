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