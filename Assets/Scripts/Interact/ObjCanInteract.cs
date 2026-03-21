using UnityEngine;

public class ObjCanInteract : Interactable, Iinteractable
{

    public bool ActiveReturn()
    {
        if (!alreadyDone && _onFogus)
        {
            Success();
            return true;
        }
        return false;
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
