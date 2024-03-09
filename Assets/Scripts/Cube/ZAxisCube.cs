using UnityEngine;
using System;

public class ZAxisCube : BaseCube
{
    [SerializeField] private Renderer _renderer;

    [SerializeField] private ZAxisMovement _movement;

    public override void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    public override void Stop(Transform lastCubePosition)
    {
        _movement.StopMove();

        float distanceDifference = GetDistanceDifference(lastCubePosition);

        if (Mathf.Abs(distanceDifference) <= distanceTrashhold)
        {
            transform.position = new Vector3(
                lastCubePosition.position.x,
                transform.position.y,
                lastCubePosition.position.z);

            EventBus<PreciseTapEvent>.Raise(new PreciseTapEvent() { CubePosition = transform});
            VibrationHandler.Vibrate();

            enabled = false;
            return;
        }

        float direction = distanceDifference > 0.0f ? 1.0f : -1.0f;

        Split(lastCubePosition, distanceDifference, direction);

        enabled = false;
    }

    public override float GetDistanceDifference(Transform lastCube)
    {
        return transform.position.z - lastCube.position.z;
    }

    public override float GetMaximumDistanceDifference() => transform.localScale.z;

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

        EventBus<CubeSplitEvent>.Raise(new CubeSplitEvent()  
        { 
            Position = newPosition, 
            Scale = newScale,
            Color = _renderer.material.color
        });
    }
}