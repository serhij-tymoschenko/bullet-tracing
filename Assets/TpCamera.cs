using UnityEngine;

public class TpCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Vector3 velocity = Vector3.zero;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        var target = cameraTarget.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, 0.1f);
        transform.LookAt(cameraTarget);
    }
}