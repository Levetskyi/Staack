using UnityEngine;

public class GameLoop : MonoBehaviour
{
    [Header("Spawners")]
    [SerializeField] private Spawner[] spawners;

    [Header("Cubes")]
    [SerializeField] private BaseCube currentCube;
    [SerializeField] private BaseCube lastCube;

    private Spawner _currentSpawner;
    private int spawnerIndex;

    private void OnEnable()
    {
        EventsHolder.OnTouch += StopCube;
        EventsHolder.OnEndLevel += StopGame;
    }

    private void OnDisable()
    {
        EventsHolder.OnTouch -= StopCube;
        EventsHolder.OnEndLevel -= StopGame;
    }

    public void StartGame()
    {
        SpawnNewCube();
    }

    private void StopGame()
    {
        currentCube.Die();

        enabled = false;
    }

    private void StopCube()
    {
        if (currentCube == null)
            return;

        if (!currentCube.IsOnAllowedPosition(lastCube))
        {
            EventsHolder.EndLevel();
            return;
        }

        currentCube.Stop(lastCube);

        lastCube = currentCube;

        SpawnNewCube();
    }

    private void SpawnNewCube()
    {
        spawnerIndex = spawnerIndex == 0 ? 1 : 0;

        _currentSpawner = spawners[spawnerIndex];

        currentCube = _currentSpawner.Spawn(lastCube.transform);
    }
}