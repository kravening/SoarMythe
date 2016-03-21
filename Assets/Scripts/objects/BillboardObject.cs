using UnityEngine;
using System.Collections;

public class BillboardObject : MonoBehaviour
{
    private Transform myTransform;

    void Awake()
    {
        myTransform = gameObject.transform;
    }

    void Update()
    {
        Vector3 v = Camera.main.transform.position - myTransform.position;
        //v.x = v.z = 0.0f;

        myTransform.LookAt(Camera.main.transform.position);
    }

}