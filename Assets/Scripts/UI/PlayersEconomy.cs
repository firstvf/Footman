using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersEconomy : MonoBehaviour
{
    public static PlayersEconomy Singleton { get; private set; }
    [SerializeField] private PlayerUI _greenTeamPlayerUI;
    [SerializeField] private PlayerUI _RedTeamPlayerUI;
    [SerializeField] private AudioSource _greenTeamAudioSource;
    [SerializeField] private AudioSource _redTeamAudioSource;
    [SerializeField] private AudioClip _buySound;
    public int GreenTeamMoney { get; private set; }
    public int GreenTeamWood { get; private set; }
    public int GreenTeamIron { get; private set; }
    public int RedTeamMoney { get; private set; }
    public int RedTeamWood { get; private set; }
    public int RedTeamIron { get; private set; }

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        RedTeamMoney = 0;
        GreenTeamMoney = 0;
    }

    public void AddMoneyGreenTeam(int money)
    {
        GreenTeamMoney += money;
        _greenTeamPlayerUI.GreenTeamShowRecources();
    }

    public void WithdrawMoneyGreenTeam(int money)
    {
        GreenTeamMoney -= money;
        _greenTeamPlayerUI.GreenTeamShowRecources();
        _greenTeamAudioSource.PlayOneShot(_buySound,0.2f);
    }

    public void AddWoodGreenTeam()
    {
        GreenTeamWood++;
        _greenTeamPlayerUI.GreenTeamShowRecources();
    }
    public void AddIronGreenTeam()
    {
        GreenTeamIron++;
        _greenTeamPlayerUI.GreenTeamShowRecources();
    }

    public void AddMoneyRedTeam(int money)
    {
        RedTeamMoney += money;
        _RedTeamPlayerUI.RedTeamShowRecources();
    }

    public void WithdrawMoneyRedTeam(int money)
    {
        RedTeamMoney -= money;
        _RedTeamPlayerUI.RedTeamShowRecources();
        _redTeamAudioSource.PlayOneShot(_buySound,0.2f);
    }

    public void AddWoodRedTeam()
    {
        RedTeamWood++;
        _RedTeamPlayerUI.RedTeamShowRecources();
    }
    public void AddIronRedTeam()
    {
        RedTeamIron++;
        _RedTeamPlayerUI.RedTeamShowRecources();
    }

    public void WithdrawRecourcesGreenTeam(int money, int iron, int wood)
    {
        GreenTeamMoney -= money;
        GreenTeamIron -= iron;
        GreenTeamWood -= wood;
        _greenTeamPlayerUI.GreenTeamShowRecources();
        _greenTeamAudioSource.PlayOneShot(_buySound,0.2f);
    }

    public void WithdrawRecourcesRedTeam(int money, int iron, int wood)
    {
        RedTeamMoney -= money;
        RedTeamIron -= iron;
        RedTeamWood -= wood;
        _RedTeamPlayerUI.RedTeamShowRecources();
        _redTeamAudioSource.PlayOneShot(_buySound,0.2f);
    }
}