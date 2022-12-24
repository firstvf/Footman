using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _units;
    [SerializeField] private Smithy _smithy;
    private int _currentUnitLevel;
    private WaitForSeconds _spawnerTimer;

    private ListUnitsPooler _pooler;
    //private ObjectPooler _poolerObj;

    private void Start()
    {
        //  _poolerObj = new ObjectPooler(_units[0], 1, transform.position);
        _pooler = new ListUnitsPooler(_units[0], 1, transform.position);
        _spawnerTimer = new WaitForSeconds(8);
        StartCoroutine(SpawnerCoroutine());
    }

    private IEnumerator SpawnerCoroutine()
    {
        while (true)
        {
            //_poolerObj.GetFreeObject();
            
            var unit = _pooler.GetFreeUnit();

            if (_smithy.IsBuild && (_smithy.ArmorUpgrade > unit.ArmorUpgrade || _smithy.WeaponUpgrade > _smithy.WeaponUpgrade))
                unit.UpgradeUnit(_smithy.ArmorUpgrade, _smithy.WeaponUpgrade);


            yield return _spawnerTimer;
        }
    }
}