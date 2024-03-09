using UnityEngine;


public abstract class BaseCube : MonoBehaviour
{
    protected float _moveSpeed = 0.0f;
    protected float distanceTrashhold = 0.05f;

    public virtual void SetMovePositions(Vector3 startPosition, Vector3 endPosition) { }

    public virtual float GetMaximumDistanceDifference() => 0.0f;

    public virtual float GetDistanceDifference(Transform lastCube) => 0.0f;

    public virtual void Stop(Transform lastCubePosition) { }

    public virtual bool IsOnAllowedPosition(BaseCube lastCube)
    {
        float distanceDifference = GetDistanceDifference(lastCube.transform);
        float maxDistanceDifference = GetMaximumDistanceDifference();

        return Mathf.Abs(distanceDifference) <= maxDistanceDifference;
    }

    public virtual void Split(Transform lastCube, float distanceDifference, float direction) { }

    public virtual void SetSpeed(float newSpeed) => _moveSpeed = newSpeed;

    public virtual void SetColor(Color color) { }

    public void Die()
    {
        _moveSpeed = 0.0f;

        gameObject.AddComponent<Rigidbody>();
    }
}