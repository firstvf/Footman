using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListUnitsPooler//<T,N> where T : MonoBehaviour
{
    private GameObject _prefab;
    private Vector3 _respawnPosition;
    private List<(GameObject, UnitParent)> _poolList;

    public ListUnitsPooler(GameObject prefab, int count, Vector3 position)
    {
        _prefab = prefab;
        _respawnPosition = position;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        _poolList = new List<(GameObject,UnitParent)>();

        for (int i = 0; i < count; i++)
            CreateObject();
    }

    private UnitParent CreateObject(bool isActiveByDefault = false)
    {
        var unit = Object.Instantiate(_prefab, _respawnPosition, Quaternion.identity);
        unit.SetActive(isActiveByDefault);
        _poolList.Add((unit,unit.GetComponent<UnitParent>()));
        return unit.GetComponent<UnitParent>();
    }

    private bool HasFreeUnit(out UnitParent unitParent)
    {
        foreach (var unit in _poolList)
            if (!unit.Item1.activeInHierarchy)
            {
                unitParent = unit.Item2;
                // unitObject = item.Value;
                unit.Item1.transform.position = _respawnPosition;
                unit.Item1.SetActive(true);
                return true;
            }
        unitParent = null;
        return false;
    }

    public UnitParent GetFreeUnit()
    {
        if (HasFreeUnit(out UnitParent unit))
            return unit;
        else
            return CreateObject(true);

        throw new System.Exception("There is no units in pool");
    }
}