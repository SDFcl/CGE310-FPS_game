using System;
using System.Collections;
using UnityEngine;

public class Gun : ItemCanPickUp
{
    [SerializeField] int ammoAmount = 10;
    [SerializeField] int damage = 1;

    [SerializeField] private float fireRate = 0.25f;
    [SerializeField] private Transform shootPoint;

    private float fireTimer;

    int currentAmmo;
    public int CurrentAmmo => currentAmmo;
    
    public Action<int> OnAmmoChange;
    public Action OnAmmoRunOut;


    protected override void Awake()
    {
        base.Awake();
        if (shootPoint == null)
        {
            Transform found = transform.Find("PlayerCamera/ShottingPoint");

            if (found != null)
                shootPoint = found;
            else
                Debug.LogWarning("ShottingPointnot found!");
        }
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;
    }

    void Start()
    {
        currentAmmo = ammoAmount;
    }

    public void Shoot()
    {
        if(fireTimer > 0f) return;

        fireTimer = fireRate;

        if (shootPoint == null)
        {
            Debug.LogWarning("shootPoint is null"); //เช็คว่ามีจุดยิงมั้ย
            return;
        }

        if(ObjectPooler.Instance == null)
        {
            Debug.LogError("ObjectPooler not found"); //เช็คว่ามี ObjectPool มั้ย
            return;
        }

        if(currentAmmo <= 0)
        {
            OnAmmoRunOut?.Invoke();
            return;
        } 

        GameObject bullet = ObjectPooler.Instance.SpawnFromPool(
            "Bullet",
            shootPoint.position,
            shootPoint.rotation
        );
        bullet.GetComponent<Bullet>().SetDamage(damage);

        currentAmmo --;
        OnAmmoChange?.Invoke(currentAmmo);
    }

    public void SetShootPoint(Transform point)
    {
        shootPoint = point;
    }
}
