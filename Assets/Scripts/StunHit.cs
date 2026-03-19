using UnityEngine;

public class StunOnHit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out IStunable stunable))
        {
            stunable.ApplyStun();
        }
    }
}
