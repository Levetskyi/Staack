using UnityEngine.InputSystem;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private TouchControls _touchControls;

    private void Awake()
    {
        _touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        EventsHolder.OnEndLevel += DisableInput;

        _touchControls.Touch.PrimaryFingerContact.started += RegisterTouch;

        _touchControls.Enable();
    }

    private void OnDisable()
    {
        EventsHolder.OnEndLevel -= DisableInput;

        _touchControls.Touch.PrimaryFingerContact.started -= RegisterTouch;

        _touchControls?.Disable();
    }

    private void RegisterTouch(InputAction.CallbackContext ctx)
    {
        EventsHolder.Touch();
    }

    private void DisableInput()
    {
        _touchControls?.Disable();
    }
}