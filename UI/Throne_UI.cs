using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throne_UI : MonoBehaviour
{
    [SerializeField] private GameObject _smithy, _woodCutter, _miner, _throneUI, _smithyUI;
    [SerializeField] private GameObject[] _throne;
    public int CurrentThroneLevel { get; private set; }
    private ParticleSystem _particles;
    private bool _isThroneUIAble = true;
    private PlayersEconomy _playersEconomy;
    private GreenTeam _greenTeam;
    private RedTeam _redTeam;
    private ThroneSounds _sound;

    private void Awake()
    {
        _sound = GetComponent<ThroneSounds>();
        _playersEconomy = PlayersEconomy.Singleton;
        _particles = GetComponent<ParticleSystem>();
        if (TryGetComponent(out GreenTeam greenTeam))
            _greenTeam = greenTeam;
        else if (TryGetComponent(out RedTeam redTeam))
            _redTeam = redTeam;
    }
    private void Update()
    {
        if (Input.GetAxis("Cancel") != 0 && _throneUI.activeInHierarchy)
            _throneUI.SetActive(false);
    }
    public void ShowThroneUI()
    {
        if (_smithyUI.activeInHierarchy)
            _smithyUI.SetActive(false);

        if (!_throneUI.activeInHierarchy && _isThroneUIAble)
            _throneUI.SetActive(true);
        else if (_throneUI.activeInHierarchy)
            _throneUI.SetActive(false);
    }

    private void OnMouseUpAsButton()
    {
        ShowThroneUI();
    }

    public void BuyWoodCutterWorker()
    {
        if (_greenTeam != null && _playersEconomy.GreenTeamMoney >= 25)
        {
            _playersEconomy.WithdrawMoneyGreenTeam(25);
            Instantiate(_woodCutter, transform.position, Quaternion.identity);
        }

        if (_redTeam != null && _playersEconomy.RedTeamMoney >= 25)
        {
            _playersEconomy.WithdrawMoneyRedTeam(25);
            Instantiate(_woodCutter, transform.position, Quaternion.identity);
        }
    }

    public void BuyMinerWorker()
    {
        if (_greenTeam != null && _playersEconomy.GreenTeamMoney >= 25)
        {
            _playersEconomy.WithdrawMoneyGreenTeam(25);
            Instantiate(_miner, transform.position, Quaternion.identity);
        }

        if (_redTeam != null && _playersEconomy.RedTeamMoney >= 25)
        {
            _playersEconomy.WithdrawMoneyRedTeam(25);
            Instantiate(_miner, transform.position, Quaternion.identity);
        }
    }

    public void BuyCastle()
    {
        if (_greenTeam != null && _playersEconomy.GreenTeamMoney >= 0 && _playersEconomy.GreenTeamWood >= 0 && _playersEconomy.GreenTeamIron >= 0)
        {
            _playersEconomy.WithdrawRecourcesGreenTeam(150, 15, 15);
            StartCoroutine(UpgradeCasle());
        }

        if (_redTeam != null && _playersEconomy.RedTeamMoney >= 150 && _playersEconomy.RedTeamWood >= 15 && _playersEconomy.RedTeamIron >= 15)
        {
            _playersEconomy.WithdrawRecourcesRedTeam(150, 15, 15);
            StartCoroutine(UpgradeCasle());
        }
    }

    private IEnumerator UpgradeCasle()
    {
        _sound.BuildSound();
        _particles.Play();
        _isThroneUIAble = false;
        _throneUI.SetActive(false);
        yield return new WaitForSeconds(10); 
        _throne[CurrentThroneLevel].SetActive(false);
        _throne[CurrentThroneLevel + 1].SetActive(true);
        CurrentThroneLevel++;
        _isThroneUIAble = true;
    }

    public void BuySmithy()
    {
        if (_greenTeam != null && _playersEconomy.GreenTeamMoney >= 0 && _playersEconomy.GreenTeamWood >= 0 && _playersEconomy.GreenTeamIron >= 0)
        {
            _playersEconomy.WithdrawRecourcesGreenTeam(50, 5, 5);
            StartCoroutine(BuildSmithy());
        }

        if (_redTeam != null && _playersEconomy.RedTeamMoney >= 50 && _playersEconomy.RedTeamWood >= 5 && _playersEconomy.RedTeamIron >= 5)
        {
            _playersEconomy.WithdrawRecourcesRedTeam(50, 5, 5);
            StartCoroutine(BuildSmithy());
        }
    }

    private IEnumerator BuildSmithy()
    {
        _sound.BuildSound();
        _smithy.SetActive(true);
        yield return new WaitForSeconds(15);
        _smithy.GetComponent<Smithy>().BuildSmithy();
    }
}