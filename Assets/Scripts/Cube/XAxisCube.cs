using UnityEngine;
using System;

public class XAxisCube : BaseCube
{
    [SerializeField] private Renderer _renderer;

    [SerializeField] private XAxisMovement _movement;

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

    public override float GetDistanceDifference(Transform lastCubePosition)
    {
        return transform.position.x - lastCubePosition.position.x;
    }

    public override float GetMaximumDistanceDifference()
    {
        return transform.localScale.x;
    }

    public override void Split(Transform lastCubePosition, float distanceDifference, float direction)
    {
        //Shrinking existing block

        float newXScale = lastCubePosition.localScale.x - MathF.Abs(distanceDifference);

        float fallingBlockScale = transform.localScale.x - newXScale;

        float newXPosition = lastCubePosition.position.x + (distanceDifference / 2f);

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
        
        EventBus<CubeSplitEvent>.Raise(new CubeSplitEvent()
        {
            Position = newPosition,
            Scale = newScale,
            Color = _renderer.material.color
        });
    }
}