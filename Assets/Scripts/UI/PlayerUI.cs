using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    //private GreenTeam _greenTeam;
    //private RedTeam _redTeam;
    [Header("gold, wood, iron")]
    [SerializeField] private TextMeshProUGUI[] _recourcesText;

    private void Awake()
    {
      //  if (TryGetComponent(out GreenTeam green))
       //     _greenTeam = green;
      //  else if (TryGetComponent(out RedTeam red))
       //     _redTeam = red;
    }

    public void GreenTeamShowRecources()
    {
        _recourcesText[0].text = PlayersEconomy.Singleton.GreenTeamMoney.ToString();
        _recourcesText[1].text = PlayersEconomy.Singleton.GreenTeamWood.ToString();
        _recourcesText[2].text = PlayersEconomy.Singleton.GreenTeamIron.ToString();
    }

    public void RedTeamShowRecources()
    {
        _recourcesText[0].text = PlayersEconomy.Singleton.RedTeamMoney.ToString();
        _recourcesText[1].text = PlayersEconomy.Singleton.RedTeamWood.ToString();
        _recourcesText[2].text = PlayersEconomy.Singleton.RedTeamIron.ToString();
    }
}
