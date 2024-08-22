using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] _path;
    private int _currentPathPoint;
    private UnitParent _unit;
    private float _speed = 2.5f;
   // private float _speed = 6.5f;
    private UnitAnimationController _animatorController;
    private bool _isRunTriggerSet;

    private void Awake()
    {
        _animatorController = GetComponent<UnitAnimationController>();
        _unit = GetComponent<UnitParent>();
    }
    private void OnEnable()
    {
        _currentPathPoint = 0;
    }

    private void Start()
    {
        if (TryGetComponent(out GreenTeam green))
            _path = PathPoints.Singleton.GetGreenTeamUnitPoints();

        if (TryGetComponent(out RedTeam red))
            _path = PathPoints.Singleton.GetRedTeamUnitPoints();
    }

    private void Update()
    {
       
        if (!_unit.IsTargetSet && _unit.CurrentHealth>0)
            MoveToDestinationPoint();
        else if (_unit.CurrentHealth > 0 && _unit.IsTargetSet && _unit.CurrentHealth>0)
            MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (Vector3.Distance(transform.position, _unit.Target.transform.position) > _unit.AttackRange - 0.2f)
        {
            if (!_isRunTriggerSet)
                _isRunTriggerSet = true;

            transform.LookAt(_unit.Target.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, _unit.Target.transform.position, _speed * Time.deltaTime);
        }
        else
        {
            if (_isRunTriggerSet)
            {
                _animatorController.IdleTrigger();
                _isRunTriggerSet = false;
            }

            transform.LookAt(_unit.Target.transform.position);
        }
    }

    private void MoveToDestinationPoint()
    {
        if (Vector3.Distance(transform.position, _path[_currentPathPoint].transform.position) > 0.25f)
        {
            if (!_isRunTriggerSet)
                _isRunTriggerSet = true;

            transform.LookAt(_path[_currentPathPoint].transform.position);
            transform.position = Vector3.MoveTowards(transform.position, _path[_currentPathPoint].transform.position, _speed * Time.deltaTime);
        }
        else
        {
            if (_path.Length > _currentPathPoint + 1)
                _currentPathPoint++;
        }
    }
}