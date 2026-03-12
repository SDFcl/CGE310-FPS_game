using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject holdingitem;
    [SerializeField] private float fireRate = 0.15f;

    private float fireTimer;

    private void Update()
    {
        fireTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && fireTimer <= 0f)
        {
            Gun gun = holdingitem.GetComponentInChildren<Gun>();
            if(gun != null){
                fireTimer = fireRate;
                gun.Shoot();
            }
        }
    }
}
