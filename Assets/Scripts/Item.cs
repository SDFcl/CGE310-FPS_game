using UnityEngine;

public class Item : MonoBehaviour , IActive
{
    public PickUpItem _pickUpItem;


    private void Awake()
    {
        _pickUpItem = FindObjectOfType<PickUpItem>();
    }

    public void Active()
    {
        _pickUpItem.setItem();
    }
}
