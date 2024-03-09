using UnityEngine;

public class PoolerPresenter : MonoBehaviour
{
    [SerializeField] private GameObject poolObject;

    private Pooler _pooler;

    private EventBinding<CubeSplitEvent> _cubeSplitEventBinding;

    private void Awake()
    {
        _pooler = new Pooler(poolObject, transform); 

        _pooler.CreatePool(2);
    }

    private void OnEnable()
    {
        _cubeSplitEventBinding = new EventBinding<CubeSplitEvent>(SpawnDropCube);

        EventBus<CubeSplitEvent>.Register(_cubeSplitEventBinding);
    }

    private void OnDisable()
    {
        EventBus<CubeSplitEvent>.Deregister(_cubeSplitEventBinding);
    }

    private void SpawnDropCube(CubeSplitEvent splitEvent)
    {
        var dropCube = _pooler.GetObject(splitEvent.Position, splitEvent.Scale);

        dropCube.GetComponent<Renderer>().material.color = splitEvent.Color;
    }
}