using UnityEngine;

public class AnimationController : MonoBehaviour {

    // Going to nee this to actually send the movements to the animatior.
    [SerializeField, Tooltip("The player's animation controller.")]
    Animator animator;

    // Define all the movement variables.
    bool isRunning, touchingGround, isGliding, hasJumped = false;

    // And throw them to the sharks.
    public bool IsRunning {
        set { isRunning = value;
        animator.SetBool("isRunning", value);
        }
        get { return isRunning; }
    }

    public bool TouchingGround {
        set {
            touchingGround = value;
            animator.SetBool("touchingGround", value);
        }
        get { return touchingGround; }
    }

    public bool IsGliding {
        set {
            isGliding = value;
            animator.SetBool("isGliding", value);
        }
        get { return isGliding; }
    }

    public bool HasJumped {
        set {
            hasJumped = value;
            animator.SetBool("hasJumped", value);
        }
        get { return hasJumped; }
    }

    public Animator Anim {
        get { return animator; }
    }

    void FixedUpdate() {
        // Just incase they're changed in the editor..
        if(animator.GetBool("isRunning") != isRunning)
            animator.SetBool("isRunning", isRunning);

        if (animator.GetBool("isFalling") != touchingGround)
            animator.SetBool("isFalling", touchingGround);

        if (animator.GetBool("isGliding") != isGliding)
            animator.SetBool("isGliding", isGliding);

        if (animator.GetBool("hasJumped") != hasJumped)
            animator.SetBool("hasJumped", hasJumped);
    }
}
