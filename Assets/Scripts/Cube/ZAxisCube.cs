using UnityEngine;
using System;

public class ZAxisCube : BaseCube
{
    [SerializeField] private Renderer _renderer;

    private bool movingForward = false;
    private Vector3 _startPosition;

    public override void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        float movement = _moveSpeed * Time.deltaTime;

        if (movingForward)
        {
            transform.Translate(Vector3.forward * movement);

            if (transform.position.z >= _startPosition.z)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.Translate(Vector3.back * movement);

            if (transform.position.z <= _startPosition.z - 3.0f)
            {
                movingForward = true;
            }
        }
    }

    public override void Stop(BaseCube lastCube)
    {
        StopAllCoroutines();

        EventsHolder.AddScore();

        float distanceDifference = GetDistanceDifference(lastCube.transform);

        if (Mathf.Abs(distanceDifference) <= distanceTrashhold)
        {
            transform.position = new Vector3(
                lastCube.transform.position.x,
                transform.position.y,
                lastCube.transform.position.z);

            EventsHolder.PreciseTap(transform);
            VibrationHandler.Vibrate();

            enabled = false;
            return;
        }

        float direction = distanceDifference > 0.0f ? 1.0f : -1.0f;

        Split(lastCube.transform, distanceDifference, direction);

        enabled = false;
    }

    public override float GetDistanceDifference(Transform lastCube)
    {
        float distanceDifference = transform.position.z - lastCube.position.z;

        return distanceDifference;
    }

    public override float GetMaximumDistanceDifference()
    {
        return transform.localScale.z;
    }

    public override void Split(Transform lastCube, float distanceDifference, float direction)
    {
        //Shrinking existing block

        float newZScale = lastCube.localScale.z - MathF.Abs(distanceDifference);

        float fallingBlockScale = transform.localScale.z - newZScale;

        float newZPosition = lastCube.position.z + (distanceDifference / 2f);

        transform.localScale = new Vector3(
            transform.localScale.x,
            transform.localScale.y,
            newZScale);

        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            newZPosition);

        //Falling block code

        float cubeEdge = transform.position.z + (newZScale / 2f * direction);

        float fallingBlockXPosition = cubeEdge + (fallingBlockScale / 2f * direction);

        SpawnDropCube(fallingBlockXPosition, fallingBlockScale);
    }

    private void SpawnDropCube(float position, float scale)
    {
        var newScale = new Vector3(
            transform.localScale.x,
            transform.localScale.y,
            scale);

        var newPosition = new Vector3(
            transform.position.x,
            transform.position.y,
            position);

        EventsHolder.CubeSplit(newPosition, newScale, _renderer.material.color);
    }
}