using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMover : MonoBehaviour
{

    public Rail rail;
    public Transform lookAt;

    private Transform thisTransform;
    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        thisTransform.position = rail.ProjectOnSegment(Vector3.zero, Vector3.forward * 20, lookAt.position);

        thisTransform.LookAt(lookAt.position);
    }
}
