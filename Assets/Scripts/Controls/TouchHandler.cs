using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TouchHandler : MonoBehaviour
{
    public static event Action<GameObject> TryGrab;
    public static event Action<Button> ControllsButtonPressed;
    public static event Action ControllButtonReleased;

    private PlayerInput _playerInput;

    private InputAction _touchPositionAction;
    private InputAction _touchPressAction;
    private Camera _mainCamera;
    private bool _controllButtonPressed;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _touchPositionAction = _playerInput.actions["TouchPosition"];
        _touchPressAction = _playerInput.actions["TouchPressed"];
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _touchPressAction.performed += OnPressed;
        _touchPressAction.canceled += TouchReleased;
    }

    private void OnDisable()
    {
        _touchPressAction.performed -= OnPressed;
        _touchPressAction.canceled -= TouchReleased;
    }

    private void TouchReleased(InputAction.CallbackContext context)
    {
        if (TryGetControllButton(out Button button))
            ControllButtonReleased?.Invoke();
    }

    private void OnPressed(InputAction.CallbackContext context)
    {
        Vector2 touchPosition = _touchPositionAction.ReadValue<Vector2>();
        GetObjectAtPosition(touchPosition);
    }

    private void GetObjectAtPosition(Vector2 position)
    {
        if (TryGrabAtPosition(position, out GameObject target))
            TryGrab?.Invoke(target);
    }

    private bool TryGrabAtPosition(Vector2 position, out GameObject target)
    {
        Ray ray = _mainCamera.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Food"))
            {
                target = hit.transform.gameObject;
                return true;
            }
        }
        target = null;
        return false;
    }

    private void Update()
    {
        if (_touchPressAction.IsPressed())
        {
            if (TryGetControllButton(out Button button))
                ControllsButtonPressed?.Invoke(button);
            else
                if (_controllButtonPressed)
            {
                ControllButtonReleased?.Invoke();
                _controllButtonPressed = false;
            }
        }
    }

    private bool TryGetControllButton(out Button button)
    {
        PointerEventData pointerEventData = new(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.CompareTag("ControllsButton"))
            {
                ControllsButtonPressed?.Invoke(raycastResults[i].gameObject.GetComponent<Button>());
                button = raycastResults[i].gameObject.GetComponent<Button>();
                _controllButtonPressed = true;
                return true;
            }
        }
        button = null;
        return false;
    }
}
