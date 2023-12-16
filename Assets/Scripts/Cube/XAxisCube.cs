using UnityEngine;
using System;

public class XAxisCube : BaseCube
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
            transform.Translate(Vector3.right * movement);

            if (transform.position.x >= _startPosition.x)
            {
                movingForward = false;
            }
        }
        else
        {
            transform.Translate(Vector3.left * movement);

            if (transform.position.x <= _startPosition.x - 3.0f)
            {
                movingForward = true;
            }
        }
    }

    public override void Stop(BaseCube lastCube)
    {
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
        float distanceDifference = transform.position.x - lastCube.position.x;

        return distanceDifference;
    }

    public override float GetMaximumDistanceDifference()
    {
        return transform.localScale.x;
    }

    public override void Split(Transform lastCube, float distanceDifference, float direction)
    {
        //Shrinking existing block

        float newXScale = lastCube.localScale.x - MathF.Abs(distanceDifference);

        float fallingBlockScale = transform.localScale.x - newXScale;

        float newXPosition = lastCube.position.x + (distanceDifference / 2f);

        transform.localScale = new Vector3(
            newXScale,
            transform.localScale.y,
        transform.localScale.z);

        transform.position = new Vector3(
            newXPosition,
                transform.position.y,
            transform.position.z);

        //Falling block code

        float cubeEdge = transform.position.x + (newXScale / 2f * direction);

        float fallingBlockXPosition = cubeEdge + (fallingBlockScale / 2f * direction);

        SpawnDropCube(fallingBlockXPosition, fallingBlockScale);
    }

    private void SpawnDropCube(float position, float scale)
    {
        var newScale = new Vector3(
            scale,
            transform.localScale.y,
            transform.localScale.z);

        var newPosition = new Vector3(
            position,
            transform.position.y,
            transform.position.z);

        EventsHolder.CubeSplit(newPosition, newScale, _renderer.material.color);
    }
}