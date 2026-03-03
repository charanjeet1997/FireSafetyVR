using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ParticleSystem[] particleSystems;
    public SprayScript sprayScript;

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
        }
    }
}
