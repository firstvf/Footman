using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler//<T> where T : MonoBehaviour
{
    private GameObject _prefab { get; }
    private Vector3 _prefabPosition { get; }

    private List<GameObject> _pool;

    public ObjectPooler(GameObject prefab, int count, Vector3 position)
    {
        _prefab = prefab;
        _prefabPosition = position;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _pool = new List<GameObject>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private GameObject CreateObject(bool isActiveByDefault = false)
    {
        var unit = Object.Instantiate(_prefab, _prefabPosition, Quaternion.identity);
        unit.SetActive(isActiveByDefault);
        _pool.Add(unit);
        return unit;
    }

    public bool HasFreeElement(out GameObject unit)
    {
        foreach (var obj in _pool)
            if (!obj.activeInHierarchy)
            {
                unit = obj;
                obj.transform.position = _prefabPosition;
                obj.SetActive(true);
                return true;
            }
        unit = null;
        return false;
    }

    public GameObject GetFreeObject()
    {
        if (HasFreeElement(out var unit))
            return unit;
        else
            return CreateObject(true);

        throw new System.Exception("There is no elements in pool ");
    }
}