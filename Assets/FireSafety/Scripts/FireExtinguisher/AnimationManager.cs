using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetGrabTrigger()
    {
        animator.Play("Press");
    }

    public void SetReleaseTrigger()
    {
        animator.Play("Release");
    }
}
