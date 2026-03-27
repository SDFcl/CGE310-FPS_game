using UnityEngine;

public class SoundHandle : MonoBehaviour
{
    private AudioSource audioSource;
   private Gun gun;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        gun = GetComponentInChildren<Gun>();
        if (gun == null)
        {
            //Debug.Log("IKD");
        }
    }

    public void AudioSFX(AudioClip clip)
    {

        audioSource.PlayOneShot(clip);
    }
    public void ShootSFX(AudioClip clip)
    {
        if (gun == null) gun = GetComponentInChildren<Gun>(true);
        if (gun.CurrentAmmo > 0)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {

        }
       
    }
}
