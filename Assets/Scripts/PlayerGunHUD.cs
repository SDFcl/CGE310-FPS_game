using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGunHUD : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Shooter")]
    [SerializeField] private PlayerShooter playerShooter;

    [Header("Ammo UI")]
    [SerializeField] private RectTransform ammoContainer;
    [SerializeField] private Image ammoBarPrefab;

    private readonly List<Image> ammoBars = new();

    private Gun currentGun;

    void Awake()
    {
        if (playerShooter == null)
        {
            playerShooter = FindAnyObjectByType<PlayerShooter>();

            if (playerShooter == null)
            {
                Debug.LogError("Scene dont have PlayerShooter");
            }
        }

        ClearContainerChildren();
    }

    void Start()
    {
        if (ammoText != null)
            ammoText.text = "";
    }

    void OnEnable()
    {
        if (playerShooter != null)
            playerShooter.OnChangeGun += OnGunChanged;
    }

    void OnDisable()
    {
        if (playerShooter != null)
            playerShooter.OnChangeGun -= OnGunChanged;

        if (currentGun != null)
            currentGun.OnAmmoChange -= AmmoChange;
    }

    void OnGunChanged(Gun gun)
    {
        if (currentGun != null)
            currentGun.OnAmmoChange -= AmmoChange;

        currentGun = gun;

        if (currentGun == null)
        {
            if (ammoText != null)
                ammoText.text = "";

            ClearBars();
            return;
        }

        currentGun.OnAmmoChange += AmmoChange;

        BuildAmmoUI(currentGun.AmmoAmount);
        AmmoChange(currentGun.CurrentAmmo);
    }

    void AmmoChange(int ammo)
    {
        if (ammoText != null)
            ammoText.text = $"{ammo} / {currentGun.AmmoAmount}";

        if (currentGun != null)
            UpdateAmmoBars(ammo, currentGun.AmmoAmount);
    }

    void BuildAmmoUI(int maxAmmo)
    {
        ClearBars();
        ClearContainerChildren();

        for (int i = 0; i < maxAmmo; i++)
        {
            Image bar = Instantiate(ammoBarPrefab, ammoContainer);
            bar.gameObject.SetActive(true);
            ammoBars.Add(bar);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(ammoContainer);
    }

    void UpdateAmmoBars(int currentAmmo, int maxAmmo)
    {
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);

        for (int i = 0; i < ammoBars.Count; i++)
        {
            int indexFromTop = ammoBars.Count - 1 - i;
            bool show = indexFromTop < currentAmmo;
            ammoBars[i].gameObject.SetActive(show);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(ammoContainer);
    }

    void ClearBars()
    {
        for (int i = 0; i < ammoBars.Count; i++)
        {
            if (ammoBars[i] != null)
                Destroy(ammoBars[i].gameObject);
        }

        ammoBars.Clear();
    }

    void ClearContainerChildren()
    {
        if (ammoContainer == null) return;

        for (int i = ammoContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(ammoContainer.GetChild(i).gameObject);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(ammoContainer);
    }
}