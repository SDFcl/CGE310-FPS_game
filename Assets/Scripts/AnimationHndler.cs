using System;
using UnityEngine;

public class AnimationHndler : MonoBehaviour
{
    private PlayerPickup playerPickup;
    private PlayerShooter playerShooter;
    private Animator animator;
    Gun currentGun;
    

    private bool IsKnife;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerPickup = GetComponent<PlayerPickup>();
        playerShooter = GetComponent<PlayerShooter>();
       
    }
    private void OnEnable()
    {
        playerPickup.OnPickup += Pickup;
        playerShooter.OnChangeGun += OnGunChanged;
    }
    private void OnDisable()
    {
        playerPickup.OnPickup -= Pickup;
        playerShooter.OnChangeGun -= OnGunChanged;
    }
    private void Update()
    {
        /*if (playerPickup != null && playerPickup.itemHolder != null)
        {
            if (Input.GetMouseButtonDown(0) && playerPickup.itemHolder.name.Contains("Gun"))
            {
                animator.SetTrigger("Shoot");
            }
        }*/
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
    void OnGunChanged(Gun gun)
    {
        // ถ้ามีปืนเก่า
        if (currentGun != null)
            currentGun.Onfire -= Fire;

        currentGun = gun;
        
        if (currentGun == null)
        {
            return;
        }

        // subscribe ammo change
        currentGun.Onfire += Fire;
    }
    void Fire()
    {
        animator.SetTrigger("Shoot");
    }

}
