using UnityEngine;

public class ItemCanPickUp : MonoBehaviour , Iinteractable
{
    public ShowItem _showItem;
    public GameObject prefabGO;


    private void Awake()
    {
        _showItem = FindObjectOfType<ShowItem>();
    }

    public bool ActiveReturn()
    {
        Debug.Log("Active ItemCanPickUp : " + gameObject.name);
        return _showItem.setItem(prefabGO);
    }

    public void Active()
    {
        
    }
}
