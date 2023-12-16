using UnityEngine;


public interface IPlatform
{
    public void Stop();
}

public class Platform : MonoBehaviour, IPlatform
{
    [SerializeField] private float moveSpeed = 1.0f;

    private void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * Vector3.forward;
    }

    public void Stop()
    {
        moveSpeed = 0.0f;
    }
}
