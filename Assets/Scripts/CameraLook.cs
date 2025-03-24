using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {
        //Camera offset
        Vector3 desiredPosition = target.position + offset;
        //Smoothdamp moves the camera smoothly to the desired target position
        Vector3 smoothedPosition = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );
        transform.position = smoothedPosition;
    }
}
