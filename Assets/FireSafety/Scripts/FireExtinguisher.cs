using System.Collections;
using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public float extinguishDelay = 2f;

    private ParticleSystem _fireParticles;
    private bool _isExtinguishing ;

    private void Awake()
    {
        _fireParticles = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (!_isExtinguishing && other.CompareTag("Extinguisher"))
        {
            StartCoroutine(StopFire());
        }
    }

    IEnumerator StopFire()
    {
        _isExtinguishing = true;

        yield return new WaitForSeconds(extinguishDelay);

        _fireParticles.Stop();
    }
}
