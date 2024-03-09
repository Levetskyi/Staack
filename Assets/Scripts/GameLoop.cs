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

    private EventBinding<TouchEvent> _touchEventBinding;


    private void OnEnable()
    {
        _touchEventBinding = new EventBinding<TouchEvent>(StopCube);
        EventBus<TouchEvent>.Register(_touchEventBinding);
    }

    private void OnDisable()
    {
        EventBus<TouchEvent>.Deregister(_touchEventBinding);
    }

    public void StartGame()
    {
        SpawnNewCube();
    }

    private void StopGame()
    {
        EventBus<EndLevelEvent>.Raise(new EndLevelEvent());

        currentCube.Die();

        enabled = false;
    }

    private void StopCube()
    {
        if (currentCube == null)
            return;

        if (!currentCube.IsOnAllowedPosition(lastCube))
        { 
            StopGame();
            return;
        }

        currentCube.Stop(lastCube.transform);

        lastCube = currentCube;

        SpawnNewCube();

        EventBus<AddScoreEvent>.Raise(new AddScoreEvent());
    }

    private void SpawnNewCube()
    {
        spawnerIndex = spawnerIndex == 0 ? 1 : 0;

        _currentSpawner = spawners[spawnerIndex];

        currentCube = _currentSpawner.Spawn(lastCube.transform);
    }
}