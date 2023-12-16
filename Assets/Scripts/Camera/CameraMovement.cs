using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float movementDuration = 1.0f;
    [SerializeField] private float movementStep = 1.0f;

    private Camera _camera;
    private Coroutine _coroutine;

    private float _lookUpHeight = 0.0f;
    private float _initialPossition;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        _initialPossition = transform.position.y;
    }

    private void OnEnable()
    {
        EventsHolder.OnTouch += StartMovement;
        EventsHolder.OnEndLevel += StartZoomOut;
    }

    private void OnDisable()
    {
        EventsHolder.OnTouch -= StartMovement;
        EventsHolder.OnEndLevel -= StartZoomOut;
    }

    private void StartZoomOut()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        StartCoroutine(ZoomOut());
    }

    private IEnumerator ZoomOut()
    {
        float initialFieldOfView = _camera.orthographicSize;
        float targetFieldOfView = initialFieldOfView + 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < 2.5f)
        {
            elapsedTime += Time.deltaTime;
            float newFieldOfView = Mathf.Lerp(initialFieldOfView, targetFieldOfView, elapsedTime);

            _camera.orthographicSize = newFieldOfView;

            yield return null;
        }

        _camera.orthographicSize = targetFieldOfView;
    }

    private void StartMovement()
    {
        _lookUpHeight += 0.25f;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(Movement());
    }

    private IEnumerator Movement()
    {
        float targetPositionY = _initialPossition + _lookUpHeight;
        float elapsedTime = 0.0f;

        while (elapsedTime < movementDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / movementDuration);
            float newPositionY = Mathf.Lerp(transform.position.y, targetPositionY, t);

            transform.position = new Vector3(
                transform.position.x, newPositionY, transform.position.z);

            yield return null;
        }

        transform.position = new Vector3(
            transform.position.x, targetPositionY, transform.position.z);
    }
}