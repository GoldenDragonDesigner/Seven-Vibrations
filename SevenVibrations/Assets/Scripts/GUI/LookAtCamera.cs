using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour
{
    [Header("This is for having world space UI's look at the camera")]
    public Camera uiCamera;

    private void Awake()
    {
        uiCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - uiCamera.transform.position);
    }
}
