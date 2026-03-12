using UnityEngine;

public class CenterRayInteract : MonoBehaviour
{
    [Header("Settings")]
    public float interactDistance = 5f;       // ระยะตรวจจับสูงสุด
    public float detectRadius = 0.5f;         // รัศมีตรวจรอบจุดเล็ง
    public LayerMask interactableLayer;

    [Header("Debug")]
    public GameObject currentTarget;

    public Camera cam;

    public ItemCanPickUp _interactable;
    private GameObject playerReference;

    void Start()
    {

    }

    void Update()
    {
        DetectNearestInteractable();
        if (Input.GetKeyDown(KeyCode.F))
        {
            Iinteractable active = currentTarget.GetComponent<Iinteractable>();
            _interactable = currentTarget.GetComponent<ItemCanPickUp>();
            if (active != null && active.ActiveReturn())
            {
                _interactable.Success();
                _interactable = null;
                currentTarget = null;
            }
        }
    }

    void DetectNearestInteractable()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit[] hits = Physics.SphereCastAll(ray, detectRadius, interactDistance, interactableLayer);

        if (hits.Length > 0)
        {
            // หาอันที่ใกล้ที่สุดจากกล้อง
            float nearestDist = Mathf.Infinity;
            GameObject nearestTarget = null;

            foreach (var hit in hits)
            {
                if (hit.collider == null) continue;

                float dist = hit.distance;
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearestTarget = hit.collider.gameObject;
                }
            }

            if (currentTarget != nearestTarget)
            {
                // ปิดของเก่า
                if (currentTarget != null)
                {
                    Iinteractable oldTarget = currentTarget.GetComponent<Iinteractable>();
                    if (oldTarget != null)
                        oldTarget.Active();
                }

                // เปิดของใหม่
                if (nearestTarget != null)
                {
                    Iinteractable newTarget = nearestTarget.GetComponent<Iinteractable>();
                    if (newTarget != null)
                        newTarget.Active();
                }
            }
            currentTarget = nearestTarget;
        }
        else
        {
            if (currentTarget != null)
            {
                Iinteractable oldTarget = currentTarget.GetComponent<Iinteractable>();
                if (oldTarget != null)
                    oldTarget.Active();
            }

            currentTarget = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (cam == null) return;

        Gizmos.color = Color.yellow;
        // แสดงเส้นจากจุดกลางจอ
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Gizmos.DrawRay(ray.origin, ray.direction * interactDistance);
        // แสดงทรงกลมปลาย ray (ประมาณจุดตรวจ)
        Gizmos.DrawWireSphere(ray.origin + ray.direction * interactDistance * 0.5f, detectRadius);
    }
}
