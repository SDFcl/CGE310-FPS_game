using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private Rigidbody[] _ragdollRigidbodies;
    private Collider[] _ragdollCollider;
    public bool ragdollEnable;
    private bool _lastRagdollState;
    void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollCollider = GetComponentsInChildren<Collider>();

        ragdollEnable = false;
        _lastRagdollState = ragdollEnable;

        DisableRagdoll();
    }
    void Update()
    {
        if (ragdollEnable != _lastRagdollState)
        {
            if (ragdollEnable)
            {
                EnableRagdoll();
            }
            else
            {
                DisableRagdoll();
            }

            _lastRagdollState = ragdollEnable;
        }
    }
    private void DisableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        foreach (var collider in _ragdollCollider)
        {
            collider.enabled = false;
        }
    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        foreach (var collider in _ragdollCollider)
        {
            collider.enabled = true;
        }
    }
}
