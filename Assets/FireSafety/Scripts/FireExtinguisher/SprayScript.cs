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
    public AnimationManager animator;
    public AudioManager audioManager;
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
        sprayAction.action.performed += OnSprayPressed;
        sprayAction.action.canceled += OnSprayCancel;
    }



    void OnDisable()
    {
        sprayAction.action.performed -= OnSprayPressed;
        sprayAction.action.canceled -= OnSprayCancel;
        sprayAction.action.Disable();
    }

    private void OnSprayCancel(InputAction.CallbackContext obj)
    {
        animator.SetReleaseTrigger();
        audioManager.StopSpraySound();
    }
    
    private void OnSprayPressed(InputAction.CallbackContext obj)
    {
        if (!grab.gameObject.CompareTag("FireExtinguisher")) return;
        if(!grab.isSelected) return;
        animator.SetGrabTrigger();
        audioManager.PlaySpraySound();
    }

    void Update()
    {
        if(socketInteractor.hasSelection) return; 
        
        float h = sprayAction.action.ReadValue<float>();

        if (!grab.gameObject.CompareTag("FireExtinguisher")) return;
        
        if (!grab.isSelected)
        {
            if (sprayParticles.isPlaying)
            { 
                sprayParticles.Stop();
            }
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
