using UnityEngine;

public class StunHit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out IStunable stunable))
        {
            stunable.ApplyStun();
        }
    }
}
