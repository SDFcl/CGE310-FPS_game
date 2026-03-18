using UnityEngine;

public class ThrowableProjectile : MonoBehaviour
{
    [Header("Impact")]
    public GameObject impactEffect;
    public AudioSource impactSound;

    private bool hasHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        hasHit = true;

        ContactPoint contact = collision.contacts[0];
        Impact(contact.point);

        // ปิดตัวเองก่อนหาย (กันชนซ้ำ)
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        Destroy(gameObject, 0.1f);
    }

    void Impact(Vector3 point)
    {
        // Effect
        if (impactEffect != null)
        {
            point += Vector3.up * 0.2f; // ยกจุดกระทบขึ้นเล็กน้อยเพื่อไม่ให้ฝังในพื้น
            GameObject fx = Instantiate(impactEffect, point, Quaternion.identity);
            fx.SetActive(true);
            Destroy(fx, 2f);
        }

        // Sound
        if (impactSound != null)
        {
            impactSound.PlayOneShot(impactSound.clip);
        }
    }
}