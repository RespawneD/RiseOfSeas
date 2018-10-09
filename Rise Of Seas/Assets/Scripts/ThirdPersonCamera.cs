using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    [SerializeField] private float mouseSensitivity = 10;
    [SerializeField] private Transform target;
    [SerializeField] float dstFromTarget = 2;
    [SerializeField] float rotationSmoothTime = .12f;
    [SerializeField] Vector2 pitchMinMax = new Vector2(-40, 85);

    float yaw, pitch;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float groundDistance;

    void LateUpdate () {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        //Get ground distance

        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.down, out hit);

        

        pitch = Mathf.Clamp(pitch, Mathf.Max(pitchMinMax.x, transform.position.y - hit.distance + 1), pitchMinMax.y);

        

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;
        transform.position = target.position - transform.forward * dstFromTarget;

	}
}
