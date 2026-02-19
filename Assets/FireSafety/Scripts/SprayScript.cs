using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SprayScript : MonoBehaviour
{
    public InputActionReference sprayAction;
    public ParticleSystem sprayParticles;
    public XRSocketInteractor socketInteractor;
    XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void Start()
    {
        sprayParticles.Stop();    
    }

    void OnEnable()
    {
        sprayAction.action.Enable();
    }

    void OnDisable()
    {
        sprayAction.action.Disable();
    }

    void Update()
    {
        if(socketInteractor.hasSelection) return; 
        
        float h = sprayAction.action.ReadValue<float>();
        
        if (!grab.isSelected)
        { 
            if(sprayParticles.isPlaying)
                    sprayParticles.Stop();
            return;
        }

        if (h > 0.1f)
        {
            sprayParticles.Play();
        }
        else
        { 
            sprayParticles.Stop();
        }
        
    }


}
