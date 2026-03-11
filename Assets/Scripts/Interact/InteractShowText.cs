using UnityEngine;
using UnityEngine.Events;

public class InteractShowText : MonoBehaviour 
{
    public bool onFogus = false;

    public GameObject goText;

    public MonoBehaviour playerReference;

    private void Awake()
    {
        if (goText != null)
        {
            goText.SetActive(false);
        }
    }

    private void Update()
    {
        if (goText != null && onFogus)
        {
            goText.SetActive(true);
        }
        else
        {
            goText.SetActive(false);
        }
        onFogus = false;

    }
}
