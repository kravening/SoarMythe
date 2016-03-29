using UnityEngine;

//[RequireComponent (typeof(BarsEffect))]
public class CameraControl : MonoBehaviour {
    [SerializeField]
    float maxDistance = 4.3f;
    [SerializeField]
    float minDistance = -0.5f;

    [SerializeField]
    float xSensitivity = 25;
    [SerializeField]
    float ySensitivity = 10;

    [SerializeField]
    float distanceAway = 3;
    [SerializeField]
    float distanceUp = 3;
    [SerializeField]
    float camSmoothDampTime = 0.1f;
    [SerializeField]
    Transform followXForm;
    float distanceFromWall = 1;
    float wideScreen = 0.2f;
    float targetingTime = 0.5f;

    Vector3 targetPosition;
    Vector3 lookDir;
    Vector3 velocityCamSmooth = Vector3.zero;

    private BarsEffect barEffect;
    private CamStates camState = CamStates.Behind;

    public enum CamStates {
        Behind,
        Target,
        Free
    }

    //smoothing and damping

    void Start() {

        lookDir = followXForm.forward;

        barEffect = GetComponent<BarsEffect>();
        if (barEffect == null) {
            //Debug.LogError("Attach a widescreen BarsEffect script to the camera", this);
        }
    }

    void OnDrawGizmos() {

    }

    void LateUpdate() {
        Vector3 characterOffset = followXForm.position + new Vector3(0f, distanceUp, 0f);
        // calculate direction from camera to player, kill Y, and normalize to give a valid direction with unit magnitude
        lookDir = characterOffset - this.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();
        Debug.DrawRay(this.transform.position, lookDir, Color.green);

        // setting the target position to be the correct offset from the hovercraft
        targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;

        compensateForWalls(characterOffset, ref targetPosition);

        smoothPosition(this.transform.position, targetPosition);
        // make sure the camera is looking the right way
        transform.LookAt(followXForm);

        //Debug.DrawRay (followXForm.position, Vector3.up * distanceUp, Color.red);
        //Debug.DrawRay(followXForm.position, -1f * followXForm.forward * distanceUp, Color.blue);
        Debug.DrawLine(followXForm.position, targetPosition, Color.magenta);
    }
    void smoothPosition(Vector3 fromPos, Vector3 toPos) {
        this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }
    void compensateForWalls(Vector3 fromObject, ref Vector3 toTarget) {
        Debug.DrawLine(fromObject, toTarget, Color.cyan);
        //compensate for walls between camera
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit)) {
            Debug.DrawRay(wallHit.point, Vector3.left, Color.red);
            toTarget = new Vector3(wallHit.point.x, toTarget.y, wallHit.point.z) + wallHit.normal * distanceFromWall;
        }
    }

    /// <summary>
    /// Makes the camera rotate on Y.
    /// </summary>
    /// <param name="strength">Negative makes it go left</param>
    public void RotateY(float strength) {
        transform.position -= transform.right * (strength / ySensitivity);
        return;
    }

    /// <summary>
    /// Makes the camera rotate on X.
    /// </summary>
    /// <param name="strength">Negative makes it go down</param>
    public void RotateX(float strength) {
        if (strength < 0) {
            if (distanceUp > minDistance) {
                //distanceAway -= (strength / xSensitivity);
                distanceUp += (strength / xSensitivity);
            }
        } else {
            if (distanceUp < maxDistance) {
                //distanceAway -= (strength / xSensitivity);
                distanceUp += (strength / xSensitivity);
            }
        }

        if (distanceUp < minDistance) {
            //distanceAway = 7.5f;
            distanceUp = minDistance;
        }

        if (distanceUp > maxDistance) {
            //distanceAway = 2.7f;
            distanceUp = maxDistance;
        }

        return;
    }
}