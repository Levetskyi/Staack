using UnityEngine;

public class TimedObjectDisabler : MonoBehaviour
{
    [SerializeField] private float destroyTime = 1f;

    [SerializeField] private Rigidbody cubeRigidbody;
    [SerializeField] private Renderer cubeRenderer;

    private float _timer;
    private bool _isCounting;

    private void OnEnable()
    {
        ResetVelocity();

        _isCounting = true;
    }

    private void ResetVelocity()
    {
        cubeRigidbody.velocity = Vector3.zero;
        cubeRigidbody.angularVelocity = Vector3.zero;
    }

    private void Update()
    {
        if (_isCounting)
        {
            _timer += Time.deltaTime;

            if (_timer >= destroyTime)
            {
                gameObject.SetActive(false); 
                _timer = 0f;
                _isCounting = false; 
            }
        }
    }
}