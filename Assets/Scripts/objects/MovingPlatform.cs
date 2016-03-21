using UnityEngine;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour {

    Transform platform;

    // The points between which the platform loops.
    [SerializeField][Tooltip("The platform will move between these points, also it will start at the first one in the array.")]
    List<Transform> points = new List<Transform>();
    
    // The container for anything that should be affected by the platform.
    List<Transform> passengers = new List<Transform>();

    // Create all the integer values I will need. They're edited in the Start(), so their
    // value has to either be 0 or will not stay that way.
    int pointsCount, lastPoint, nextPoint = 0;

    // The speed at which the platform moves.
    [SerializeField][Range(0,0.1f)][Tooltip("How fast the platform will move.")]
    float speed = 0.1f;

    void Start() {
        pointsCount = points.Count - 1;
        nextPoint++;
        platform = GetComponent<Transform>();
    }

    void Update() {
        // This for loop would just grab resources to make two variables it won't use if this is outside the editor.
        # if UNITY_EDITOR
        for(int i = 0; i < points.Count; i++) {
            Transform point = points[i];
            Transform next = i + 1 >= points.Count ? points[0] : points[i + 1];
            Debug.DrawLine(point.position, next.position, Color.red, 0, false);
        }
        #endif

        if (Vector3.Distance(platform.position, points[nextPoint].position) > 0.5f) {
            platform.position = Vector3.MoveTowards(platform.position, points[nextPoint].position, speed);
            for (int i = 0; i < passengers.Count; i++) {
                Transform passenger = passengers[i];
                Vector3 customPoint = points[nextPoint].position;
                customPoint.y = passenger.position.y;
                passenger.position = Vector3.MoveTowards(passenger.position, customPoint, speed);
            }
        } else {
            if (lastPoint >= pointsCount) {
                lastPoint = 0;
            } else {
                lastPoint++;
            }
            if (nextPoint >= pointsCount) {
                nextPoint = 0;
            } else {
                nextPoint++;
            }
        }
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log(other.collider.name);
        if (passengers.IndexOf(other.transform) == -1) {
            passengers.Add(other.transform);
        }
    }

    void OnCollisionExit(Collision other) {
        if (passengers.IndexOf(other.transform) != -1) {
            passengers.Remove(other.transform);
        }
    }
}