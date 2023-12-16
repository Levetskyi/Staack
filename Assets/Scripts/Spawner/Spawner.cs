using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private MoveDirection moveDirection;
    [SerializeField] private BaseCube cubePrefab;

    [SerializeField] private Transform endPosition;

    private float _currentSpeed = 1.5f;
    private readonly float _speedStep = 0.05f;

    private IColorGenerator _colorGenerator;

    private void Awake()
    {
        _colorGenerator = new HueColorGenerator();
    }

    public BaseCube Spawn(Transform lastCube)
    {
        var cube = Instantiate(cubePrefab);

        cube.SetSpeed(GetNewSpeed());

        cube.SetColor(_colorGenerator.GenerateNewColor());

        cube.transform.position = GetNewSpawnPosition(lastCube.position);

        cube.transform.localScale = new Vector3(
            lastCube.transform.localScale.x,
            cube.transform.localScale.y,
            lastCube.transform.localScale.z);

        return cube;
    }

    private Vector3 GetNewSpawnPosition(Vector3 lastCubePosition)
    {
        float newXPosition = moveDirection == MoveDirection.X ? transform.position.x : lastCubePosition.x;
        float newZPosition = moveDirection == MoveDirection.Z ? transform.position.z : lastCubePosition.z;

        Vector3 spawnPosition = new(
            newXPosition,
            lastCubePosition.y + cubePrefab.transform.localScale.y,
            newZPosition
        );

        return spawnPosition;
    }

    private float GetNewSpeed()
    {
        return _currentSpeed += _speedStep;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}