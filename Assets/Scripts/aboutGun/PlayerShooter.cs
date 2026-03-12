using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject holdingitem;

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Gun gun = holdingitem.GetComponentInChildren<Gun>();
            if(gun != null){
                gun.Shoot();
            }
        }
    }
}
