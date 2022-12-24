using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private GameObject _recourcesUI;
    private GameObject[] _workPath;
    private WaitForSeconds _workCooldown, _hideUIColdoown;
    private Miner _miner;
    private Woodcutter _woodcutter;
    private WorkerAnimationController _animationController;
    private GreenTeam _greenTeam;
    private RedTeam _redTeam;
    private bool _isWorking;
    private bool _isCoroutineStart;
    private int _currentPath;

    private void Awake()
    {
        _animationController = GetComponent<WorkerAnimationController>();

        if (TryGetComponent(out Miner miner))
            _miner = miner;
        else if (TryGetComponent(out Woodcutter woodcutter))
            _woodcutter = woodcutter;

        if (TryGetComponent(out GreenTeam greenTeam))
            _greenTeam = greenTeam;
        else if (TryGetComponent(out RedTeam redTeam))
            _redTeam = redTeam;
    }

    private void Start()
    {
        if (_miner != null && _greenTeam != null)
            _workPath = PathPoints.Singleton.GetGreenMinerPath();
        else if (_miner != null && _redTeam != null)
            _workPath = PathPoints.Singleton.GetRedMinerPath();

        if (_woodcutter != null && _greenTeam != null)
            _workPath = PathPoints.Singleton.GetGreenWoodcutterPath();
        else if (_woodcutter != null && _redTeam != null)
            _workPath = PathPoints.Singleton.GetRedWoodcutterPath();

        _workCooldown = new WaitForSeconds(5);
        _hideUIColdoown = new WaitForSeconds(1.5f);        

        //  StartCoroutine(Work());
    }

    private void Update()
    {
        WorkerPath();
    }

    private void WorkerPath()
    {
        if (!_isWorking && Vector3.Distance(transform.position, _workPath[_currentPath].transform.position) > 0.1f)
        {
            transform.LookAt(_workPath[_currentPath].transform.position);
            transform.position = Vector3.MoveTowards(transform.position, _workPath[_currentPath].transform.position, 2 * Time.deltaTime);

        }
        else if (!_isCoroutineStart && Vector3.Distance(transform.position, _workPath[0].transform.position) <= 0.1f)
            StartCoroutine(Work());
        else if (!_isWorking && Vector3.Distance(transform.position, _workPath[1].transform.position) <= 0.1f)
        {
            StartCoroutine(PutRecources());
            _currentPath = 0;
        }
    }
    private IEnumerator Work()
    {
        _isCoroutineStart = true;
        _isWorking = true;
        _animationController.WorkAnimation();
        yield return _workCooldown;
        _isWorking = false;
        _currentPath = 1;
        _animationController.RunAnimation();
    }

    private IEnumerator PutRecources()
    {
        _isWorking = false;
        _isCoroutineStart = false;
        _currentPath = 0;
       // _recourcesUI.transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
        _recourcesUI.SetActive(true);
        if (_miner != null)
            PutIron();
        else if (_woodcutter != null)
            PutWood();
        yield return _hideUIColdoown;
        _recourcesUI.SetActive(false);
    }

    private void PutWood()
    {
        if (_greenTeam != null)
            PlayersEconomy.Singleton.AddWoodGreenTeam();
        else if (_redTeam != null)
            PlayersEconomy.Singleton.AddWoodRedTeam();
    }

    private void PutIron()
    {
        if (_greenTeam != null)
            PlayersEconomy.Singleton.AddIronGreenTeam();
        else if (_redTeam != null)
            PlayersEconomy.Singleton.AddIronRedTeam();
    }  
}