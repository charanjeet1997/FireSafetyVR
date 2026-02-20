using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip spraySound;

    [Header("Audio Sources")]
    public AudioSource extinguisher;
    
    public void PlaySpraySound()
    {
        extinguisher.PlayOneShot(spraySound);
    }

    public void StopSpraySound()
    {
        extinguisher.Stop();
    }
    
    
    
    
}
