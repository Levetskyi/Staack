using System.Collections.Generic;
using UnityEngine;

public class Pooler
{ 
    private readonly Transform _parent;
    private readonly GameObject _object;
    private readonly List<GameObject> _objectPool = new();

    public Pooler(GameObject poolObject, Transform parent = null)
    {
        _object = poolObject;
        _parent = parent;
    }

    public void CreatePool(int size = 1)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject obj = Object.Instantiate(_object, _parent);

            obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            _objectPool.Add(obj);
        }
    }

    public GameObject GetObject(Vector3 position, Vector3 scale)
    {
        for (int i = 0; i < _objectPool.Count; i++)
        {
            if (!_objectPool[i].activeInHierarchy)
            {
                _objectPool[i].transform.position = position;
                _objectPool[i].transform.localScale = scale; 
                _objectPool[i].SetActive(true);
                return _objectPool[i];
            }
        }

        GameObject newObject = CreateObject();
        newObject.transform.position = position;
        newObject.transform.localScale = scale;

        newObject.SetActive(true);
        return newObject;
    }

    private GameObject CreateObject()
    {
        GameObject obj = Object.Instantiate(_object, _parent);

        obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        obj.SetActive(false);
        _objectPool.Add(obj);
        return obj;
    }
}