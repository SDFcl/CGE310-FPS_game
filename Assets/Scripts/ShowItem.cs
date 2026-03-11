using UnityEngine;

public class ShowItem : MonoBehaviour
{
    private bool _isPickup = false;

    public GameObject itemHolder;
    public Transform itemHoldPoint;

    public bool setItem(GameObject item)
    {
        if (!_isPickup)
        {
            _isPickup = true;

            GameObject itemHold = Instantiate(item, itemHoldPoint.position, itemHoldPoint.rotation, itemHoldPoint);
            itemHolder = itemHold;

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