using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ParticleSystem[] particleSystems;
    public SprayScript sprayScript;
    public MeshRenderer fireExtinguisherNozzleMeshRenderer;
    
    private void Awake()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
    }

    public void SpawnFireExtinguisher(int index)
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Stop();
            if (index == i)
            {
                sprayScript.sprayParticles=particleSystems[i];
            }

            if (index == 0)
            {
                fireExtinguisherNozzleMeshRenderer.enabled = true;
            }
            else
            {
                fireExtinguisherNozzleMeshRenderer.enabled = false;
            }
        }
    }
}
