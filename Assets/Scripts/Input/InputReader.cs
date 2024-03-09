using UnityEngine.InputSystem;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private TouchControls _touchControls;

    private EventBinding<EndLevelEvent> _endLevelEventBinding;

    private void Awake()
    {
        _touchControls = new TouchControls();
    }

    private void OnEnable()
    {
        _endLevelEventBinding = new EventBinding<EndLevelEvent>(DisableInput);
        EventBus<EndLevelEvent>.Register(_endLevelEventBinding);

        _touchControls.Touch.PrimaryFingerContact.started += RegisterTouch;

        _touchControls.Enable();
    }

    private void OnDisable()
    {
        EventBus<EndLevelEvent>.Deregister(_endLevelEventBinding);

        _touchControls.Touch.PrimaryFingerContact.started -= RegisterTouch;

        _touchControls?.Disable();
    }

    private void RegisterTouch(InputAction.CallbackContext ctx)
    {
        EventBus<TouchEvent>.Raise(new TouchEvent());
    }

    private void DisableInput()
    {
        _touchControls?.Disable();
    }
}