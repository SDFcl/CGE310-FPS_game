using UnityEngine;

public class UIStateAnimHandle : MonoBehaviour
{
    Animator[] animators;

    void Awake()
    {
        animators = GetComponentsInChildren<Animator>();
    }
    public void EnadleAnim(bool EnadleAnim)
{
    for (int i = 0; i < animators.Length; i++)
    {
        animators[i].SetBool("ActiveAnim", EnadleAnim);
    }
}
}
