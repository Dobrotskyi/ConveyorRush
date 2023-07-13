using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchHandler : MonoBehaviourSingleton<TouchHandler>
{
    public event Action<GameObject> TryGrab;

    private PlayerInput _playerInput;

    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;
    private Camera _mainCamera;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchPositionAction = _playerInput.actions["TouchPosition"];
        _touchPressAction = _playerInput.actions["TouchPressed"];
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _touchPressAction.performed += TryToGrab;
    }

    private void OnDisable()
    {
        _touchPressAction.performed -= TryToGrab;
    }

    private void TryToGrab(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = _touchPositionAction.ReadValue<Vector2>();
        GetObjectAtPosition(touchPosition);
    }

    private void GetObjectAtPosition(Vector2 position)
    {
        Ray ray = _mainCamera.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Food"))
            {
                Debug.Log(hit.transform.gameObject.name);
                TryGrab?.Invoke(hit.transform.gameObject);
            }
        }
    }
}
