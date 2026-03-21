using UnityEngine;

public class MeleeHit : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool isTrigger;

    private Transform ownerRoot;

    private void Awake()
    {
        ownerRoot = transform.root; // ดึง root ของตัวเอง
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTrigger) return;
        ProcessHit(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTrigger) return;
        ProcessHit(collision.collider);
    }

    private void ProcessHit(Collider col)
    {
        // 🔥 กันโดนตัวเอง (ทั้งตัว + ลูกทั้งหมด)
        if (col.transform.root == ownerRoot|| col.gameObject.CompareTag(ownerRoot.tag)) return;

        // 🔥 หา IDamageable จาก parent (สำคัญมาก)
        IDamageable damageable = col.GetComponentInParent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}