using System.Collections;
using UnityEngine;

public class ZAxisMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float movementDistance = 1.0f;

    private Coroutine _currentRoutine;

    public void StopMove() => StopCoroutine(_currentRoutine);

    private void Start() => _currentRoutine = StartCoroutine(MoveCoroutine());

    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            while (transform.position.z < movementDistance)
            {
                transform.Translate(movementSpeed * Time.deltaTime * Vector3.forward);
                yield return null;
            }

            while (transform.position.z > -movementDistance)
            {
                transform.Translate(movementSpeed * Time.deltaTime * Vector3.back);
                yield return null;
            }
        }
    }
}