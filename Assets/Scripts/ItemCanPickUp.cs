using UnityEngine;

public class ItemCanPickUp : Interactable , Iinteractable
{
    private ShowItem _showItem;
    private GameObject thisGameObj;


    private void Awake()
    {
        _showItem = FindObjectOfType<ShowItem>();
        thisGameObj = this.gameObject;
        goHightLight.SetActive(false);
    }

    public bool ActiveReturn()
    {
        Debug.Log("Active ItemCanPickUp : " + gameObject.name);
        return _showItem.setItem(thisGameObj);
    }

    public void Active()
    {

        onFogus();
    }

    public void onFogus()
    {
        if (!alreadyDone && goHightLight != null)
        {
            _onFogus = !_onFogus;
            goHightLight.SetActive(_onFogus);
        }
    }
}
