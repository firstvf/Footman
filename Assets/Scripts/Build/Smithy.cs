using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smithy : MonoBehaviour
{
    public int WeaponUpgrade { get; private set; }
    public int ArmorUpgrade { get; private set; }

    [SerializeField] private GameObject _smithyUI;
    [SerializeField] private GameObject _throneUI;

    public bool IsBuild { get; private set; }
    private GreenTeam _greenTeam;
    private RedTeam _redTeam;

    private void Awake()
    {
        if (TryGetComponent(out GreenTeam greenTeam))
            _greenTeam = greenTeam;

        if (TryGetComponent(out RedTeam redTeam))
            _redTeam = redTeam;
    }

    private void Update()
    {
        if (Input.GetAxis("Cancel") != 0 && _smithyUI.activeInHierarchy)
            _smithyUI.SetActive(false);
    }

    public void CloseSmithyUI()
    {
        _smithyUI.SetActive(false);
    }

    public void BuildSmithy()
    {
        GetComponent<MeshRenderer>().enabled = true;
        IsBuild = true;
    }

    private void OnMouseUpAsButton()
    {
        if (IsBuild)
        {
            if (_throneUI.activeInHierarchy)
                _throneUI.SetActive(false);

            if (!_smithyUI.activeInHierarchy)
                _smithyUI.SetActive(true);
            else _smithyUI.SetActive(false);
        }
    }

    public void UpgradeWeaponSwordMan()
    {
        if (_greenTeam != null && WeaponUpgrade < 3 && PlayersEconomy.Singleton.GreenTeamMoney >= 10)
        {
            WeaponUpgrade++;
            PlayersEconomy.Singleton.WithdrawMoneyGreenTeam(10);
        }
        else if (_redTeam != null && WeaponUpgrade < 3 && PlayersEconomy.Singleton.RedTeamMoney >= 10)
        {
            WeaponUpgrade++;
            PlayersEconomy.Singleton.WithdrawMoneyRedTeam(10);
        }
    }

    public void UpgradeArmorSwordMan()
    {
        if (_greenTeam != null && ArmorUpgrade < 5 && PlayersEconomy.Singleton.GreenTeamMoney >= 10)
        {
            ArmorUpgrade++;
            PlayersEconomy.Singleton.WithdrawMoneyGreenTeam(10);
        }
        else if (_redTeam != null && ArmorUpgrade < 5 && PlayersEconomy.Singleton.RedTeamMoney >= 10)
        {
            ArmorUpgrade++;
            PlayersEconomy.Singleton.WithdrawMoneyRedTeam(10);
        }
    }
}