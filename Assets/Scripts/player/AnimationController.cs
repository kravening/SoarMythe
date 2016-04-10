using UnityEngine;

public class AnimationController : MonoBehaviour {

    // Going to nee this to actually send the movements to the animatior.
    [SerializeField, Tooltip("The player's animation controller.")]
    Animator animator;

    // Define all the movement variables.
    [SerializeField]
    bool isRunning, touchingGround, isGliding, hasJumped, charging = false;
    // I could use hasJumped & charging as a trigger instead.
    // But I'm already neck deep in the way it works now.
    // For my sanity's sake it's better to keep it like this.
    // It seems SetBool also functions as SetTrigger if the value given is true.
    // Nice. I've converted both to a trigger then.

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

    public bool Charging {
        set {
            charging = value;
            SetBool("charging", value);
        }
        get { return hasJumped; }
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

    void SetTrigger(string i) {
        animator.SetTrigger(i);
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

        if (GetBool("charging") != charging)
            SetBool("charging", charging);
    }
}
