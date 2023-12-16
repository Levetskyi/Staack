using UnityEngine;

public class PoolerPresenter : MonoBehaviour
{
    [SerializeField] private GameObject poolObject;

    private Pooler _pooler;

    private void Awake()
    {
        _pooler = new Pooler(poolObject, transform); 

        _pooler.CreatePool(2);
    }

    private void OnEnable()
    {
        EventsHolder.OnCubeSplit += SpawnDropCube;
    }

    private void OnDisable()
    {
        EventsHolder.OnCubeSplit -= SpawnDropCube;
    }

    private void SpawnDropCube(Vector3 spawnPosition, Vector3 spawnScale, Color spawnColor)
    {
        var dropCube = _pooler.GetObject(spawnPosition, spawnScale);

        dropCube.GetComponent<Renderer>().material.color = spawnColor;
    }
}