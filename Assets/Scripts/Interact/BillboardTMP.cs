using UnityEngine;

public class BillboardTMP : MonoBehaviour
{
    private Transform cam;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        Vector3 direction = cam.position - transform.position;
        direction.y = 0; // ล็อกแกน Y (ไม่ให้เงย/ก้ม)

        transform.rotation = Quaternion.LookRotation(direction);
    }
}