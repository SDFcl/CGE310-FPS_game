using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractShowText : MonoBehaviour
{

    public bool _onFogus = false;
    public bool alreadyDone = false;

    public GameObject goText;

    public MonoBehaviour playerReference;

    public UnityEvent onSuccess;

    private void Awake()
    {
        if (goText != null)
        {
            goText.SetActive(false);
        }
    }

    public void Success()
    {
        alreadyDone = true;
        onSuccess.Invoke();
    }

    private void Update()
    {
        _onFogus = false;
        onFogus();
    }
    public MonoBehaviour onFogus()
    {
        if (!alreadyDone)
        {
            if (goText != null && _onFogus)
            {
                goText.SetActive(true);

                return playerReference;
            }
            else
            {
                goText.SetActive(false);
                return null;
            }
        }

        return null;
    }


}
