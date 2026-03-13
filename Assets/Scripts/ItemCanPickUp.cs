using UnityEngine;

public class ItemCanPickUp : Interactable , Iinteractable
{
    private PlayerPickup _playerPickup;
    private GameObject thisGameObj;


    protected virtual void Awake()
    {
        _playerPickup = FindObjectOfType<PlayerPickup>();
        thisGameObj = this.gameObject;
        goHightLight.SetActive(false);
    }

    public bool ActiveReturn()
    {
        Debug.Log("Active ItemCanPickUp : " + gameObject.name);
        return _playerPickup.setItem(thisGameObj);
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
