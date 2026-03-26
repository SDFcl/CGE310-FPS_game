using System;
using UnityEngine;

public class AnimationHndler : MonoBehaviour
{
   private PlayerPickup playerPickup;
    private Animator animator;
    

    private bool IsKnife;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerPickup = GetComponent<PlayerPickup>();
       
    }
    private void OnEnable()
    {
        playerPickup.OnPickup += Pickup;
    }
    private void OnDisable()
    {
        playerPickup.OnPickup -= Pickup;
    }
    private void Update()
    {
        if (playerPickup != null && playerPickup.itemHolder != null)
        {
            if (Input.GetMouseButtonDown(0) && playerPickup.itemHolder.name.Contains("Gun"))
            {
                animator.SetTrigger("Shoot");
            }
        }
    }
    void Pickup()
    {
        string itemName = playerPickup.itemHolder.name;
       
        animator.SetTrigger("PickUp");
        if (playerPickup.itemHolder.name.Contains("Knife"))
        {
            IsKnife = true;
        }
        else if (playerPickup.itemHolder.name.Contains("Gun"))
        {
            IsKnife = false;
            animator.SetTrigger("HoldGun");
        }
        else
        {
            IsKnife = false;
        }

        animator.SetBool("ISKnife",IsKnife);
    }

}
