using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float cameraSmoothing = 10.0f;
    public Vector3 offset;

    private void Start()
    {
        offset = transform.position - GlobalVariables.PLAYER.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 pinpoint = GlobalVariables.PLAYER.transform.position + offset;
        Vector3 softPosition = Vector3.Slerp(transform.position, pinpoint, cameraSmoothing);

        transform.position = softPosition;
    }
}
