using TMPro;
using UnityEngine;

public class PlayerGunHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private PlayerShooter playerShooter;

    Gun currentGun;

    void Start()
    {
        ammoText.text = "";
    }

    void OnEnable()
    {
        playerShooter.OnChangeGun += OnGunChanged;
    }

    void OnDisable()
    {
        playerShooter.OnChangeGun -= OnGunChanged;
    }

    void OnGunChanged(Gun gun)
    {
        // ถ้ามีปืนเก่า
        if (currentGun != null)
            currentGun.OnAmmoChange -= AmmoChange;

        currentGun = gun;

        if (currentGun == null)
        {
            ammoText.text = "";
            return;
        }

        // subscribe ammo change
        currentGun.OnAmmoChange += AmmoChange;

        // แสดง ammo ตอนหยิบปืน
        AmmoChange(currentGun.CurrentAmmo);
    }

    void AmmoChange(int ammo)
    {
        ammoText.text = $"Ammo : {ammo}";
    }
}