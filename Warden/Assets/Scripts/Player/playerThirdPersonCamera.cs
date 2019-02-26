using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerThirdPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 5f;

    public float rotationSmoothTime = 0.1f;
    Vector3 rotationSmmothVelocity;
    Vector3 currentRotation;

    float yaw; //Camera's rotation on the y axis
    float pitch; //Camera's rotation on the x axis

    public Vector2 pitchMinMax = new Vector2(-19, 85);

    public Transform target;
    public float distanceFromTarget = 4;

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmmothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
