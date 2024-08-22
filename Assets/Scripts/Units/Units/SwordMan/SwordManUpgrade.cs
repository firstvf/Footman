using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject[] _armorupgrade;
    [SerializeField] private GameObject[] _weaponUpgrade;

    public void UpgradeSwordMan(int weapon, int armor)
    {
        if (armor > 0)
        {
            _armorupgrade[1].SetActive(true);

            if (armor > 1)
            {
                _armorupgrade[0].SetActive(false);
                _armorupgrade[armor].SetActive(true);
            }
        }

        if (weapon > 0)
        {
            _weaponUpgrade[weapon - 1].SetActive(false);
            _weaponUpgrade[weapon].SetActive(true);
        }
    }
}