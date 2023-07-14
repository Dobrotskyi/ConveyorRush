using UnityEngine;

public class UILookAtCamera : MonoBehaviour
{
    private Transform _mainCameraTransform;
    private void Awake()
    {
        _mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + _mainCameraTransform.rotation * Vector3.forward, _mainCameraTransform.rotation * Vector3.up);
    }
}
