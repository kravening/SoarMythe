using UnityEngine;

public class AnimationController : MonoBehaviour {

    // Going to nee this to actually send the movements to the animatior.
    [SerializeField, Tooltip("The player's animation controller.")]
    Animator animator;

    // Define all the movement variables.
    [SerializeField]
    bool isRunning, touchingGround, isGliding, hasJumped = false;
    // I could use hasJumped as a trigger instead.
    // But I'm already neck deep in the way it works now.
    // For my sanity's sake it's better to keep it like this.

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
            SetBool("touchingGround", value);
        }
        get { return touchingGround; }
    }

    public bool IsGliding {
        set {
            isGliding = value;
            SetBool("isGliding", value);
        }
        get { return isGliding; }
    }

    public bool HasJumped {
        set {
            hasJumped = value;
            SetBool("hasJumped", value);
        }
        get { return hasJumped; }
    }

    public Animator Anim {
        get { return animator; }
    }

    bool GetBool(string i) {
        return animator.GetBool(i);
    }

    void SetBool(string i, bool val) {
        animator.SetBool(i, val);
    }

    void FixedUpdate() {
        // Just incase they're changed in the editor..
        if(GetBool("isRunning") != isRunning)
            SetBool("isRunning", isRunning);

        if (GetBool("touchingGround") != touchingGround)
            SetBool("touchingGround", touchingGround);

        if (GetBool("isGliding") != isGliding)
            SetBool("isGliding", isGliding);

        if (GetBool("hasJumped") != hasJumped)
            SetBool("hasJumped", hasJumped);
    }
}
