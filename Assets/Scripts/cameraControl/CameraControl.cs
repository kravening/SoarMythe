using UnityEngine;

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

    //smoothing and damping

    void Start() {
        lookDir = followXForm.forward;
    }

    void LateUpdate() {
        Vector3 characterOffset = followXForm.position + new Vector3(0f, distanceUp, 0f);
        // Calculate direction from camera to player, kill Y, and normalize to give a valid direction with unit magnitude
        lookDir = characterOffset - this.transform.position;
        lookDir.y = 0;
        lookDir.Normalize();
        // Line to show how far away from player
        Debug.DrawRay(this.transform.position, lookDir, Color.green);

        // setting the target position to be the correct offset from the hovercraft
        targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;
        // Run the void compensateForWalls
        compensateForWalls(characterOffset, ref targetPosition);

        smoothPosition(this.transform.position, targetPosition);
        // make sure the camera is looking the right way
        transform.LookAt(followXForm);
        // Line on which the camera should position itself
        Debug.DrawLine(followXForm.position, targetPosition, Color.magenta);
    }
    void smoothPosition(Vector3 fromPos, Vector3 toPos) {
        // Smooth moves the camera
        this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
    }
    void compensateForWalls(Vector3 fromObject, ref Vector3 toTarget) {
        // Point the camera looks towards
        Debug.DrawLine(fromObject, toTarget, Color.cyan);
        // Compensate for walls between camera
        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit)) {
            Debug.DrawRay(wallHit.point, Vector3.left, Color.red);
            // Stops camera from moving through the wall
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

                distanceUp += (strength / xSensitivity);
            }
        } else {
            if (distanceUp < maxDistance) {

                distanceUp += (strength / xSensitivity);
            }
        }
        if (distanceUp < minDistance) {

            distanceUp = minDistance;
        }
        if (distanceUp > maxDistance) {

            distanceUp = maxDistance;
        }
        return;
    }
}