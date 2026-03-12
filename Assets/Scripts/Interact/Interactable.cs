using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public bool _onFogus = false;
    public bool alreadyDone = false;

    public GameObject goHightLight;

    public UnityEvent onSuccess;

    private void Awake()
    {
        if (goHightLight != null)
        {
            goHightLight.SetActive(false);
        }
    }

    public void Success()
    {
        alreadyDone = true;
        onSuccess.Invoke();
    }
    


}
