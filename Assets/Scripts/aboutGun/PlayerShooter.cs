using System;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform holdingItem;
    public Gun CurrentGun { get; private set; }
    private PlayerPickup playerPickup;
    private PlayerThrowItem playerThrowItem;
    public Action<Gun> OnChangeGun;
    void Awake()
    {
        playerPickup = GetComponent<PlayerPickup>();
        playerThrowItem = GetComponent<PlayerThrowItem>();
        // ถ้าไม่ได้ assign ใน Inspector
        if (holdingItem == null)
        {
            Transform found = transform.Find("HoldingItem");

            if (found != null)
                holdingItem = found;
            else
                Debug.LogWarning("HoldingItem not found!");
        }
    }
    void OnEnable()
    {
        playerPickup.OnPickup += PickGun;
        playerThrowItem.OnThrow += ThorwGun;
    }
    void OnDisable()
    {
        playerPickup.OnPickup -= PickGun;
        playerThrowItem.OnThrow -= ThorwGun;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (CurrentGun != null)
                CurrentGun.Shoot();
        }
    }

    void PickGun()
    {
        Gun gun = holdingItem.GetComponentInChildren<Gun>();
         if (gun != CurrentGun)
            {
                CurrentGun = gun;
                OnChangeGun?.Invoke(CurrentGun);
            }
    }

    void ThorwGun()
    {
        CurrentGun = null;
        OnChangeGun?.Invoke(CurrentGun);
    }
}