using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                // Hero or object to follow
    public Vector3 offset = new Vector3(0f, 0f, 0f);  // Distance from hero (Y = height, Z = back)
    public float smoothSpeed = 5f;          // Smoothness of camera movement
    public bool lookAtTarget = true;        // Whether camera looks at the hero

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate desired position behind and above the hero
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Smooth camera motion
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Make camera face the hero (optional)
        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}
