using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int bulletAmount = 10;
    [SerializeField] int damage = 1;
    [SerializeField] private Transform shootPoint;

    int currentBullet;
    public Action<int> OnChange;
    public Action OnRunOut;


    void Awake()
    {
        if (shootPoint == null)
        {
            shootPoint = GameObject.Find("ShottingPoint")?.transform;
        }
    }
    void Start()
    {
        currentBullet = bulletAmount;
    }

    public void Shoot()
    {
        if (shootPoint == null)
        {
            Debug.LogWarning("shootPoint is null");
            return;
        }

        if(ObjectPooler.Instance == null)
        {
            Debug.LogError("ObjectPooler not found");
            return;
        }

        if(currentBullet <= 0)
        {
            OnRunOut?.Invoke();
            return;
        } 

        GameObject bullet = ObjectPooler.Instance.SpawnFromPool(
            "Bullet",
            shootPoint.position,
            shootPoint.rotation
        );
        bullet.GetComponent<Bullet>().SetDamage(damage);

        currentBullet --;
        OnChange?.Invoke(currentBullet);
    }

    public void SetShootPoint(Transform point)
    {
        shootPoint = point;
    }
}
