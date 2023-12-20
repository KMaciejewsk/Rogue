using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 5.0f;

    private Transform target;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        Camera.main.orthographicSize = 10;
    }

    void Update()
    {
        LookAtTarget();
    }

    void LookAtTarget()
    {
        Vector3 desiredPosition = target.position + new Vector3(0, 0, -10);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
