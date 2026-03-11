using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    public ShowItem showItem;
    public float throwForce = 10f;

    public Camera cam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // ปาด้วยปุ่ม G
        {
            Throw();
        }
    }

    void Throw()
    {
        GameObject item = showItem.TakeItem();

        if (item == null) return;

        item.transform.SetParent(null);

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = item.AddComponent<Rigidbody>();
        }

        rb.isKinematic = false;
        rb.AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);

    }
}