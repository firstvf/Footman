using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitParent : MonoBehaviour, IDamageable
{
    abstract public float TargetDetectionRadius { get; protected set; }
    abstract public float AttackRange { get; protected set; }
    abstract public float AttackSpeed { get; protected set; }
    abstract public float Armor { get; protected set; }
    abstract public int WeaponUpgrade { get; protected set; }
    abstract public int ArmorUpgrade { get; protected set; }
    abstract public int MaxHealth { get; protected set; }
    abstract public int CurrentHealth { get; protected set; }
    abstract public int Damage { get; protected set; }
    public UnitParent Target { get; private set; }
    public bool IsTargetSet { get; private set; }

    //[SerializeField] private GameObject _mainPrefab;

    private UnitAnimationController _animationController;
    private WaitForSeconds _attackSpeed;
    private GreenTeam _greenTeam;
    private HealthBar _hpBar;
    private RedTeam _redTeam;

    virtual protected void Awake()
    {
        if (TryGetComponent(out UnitAnimationController animationController))
            _animationController = animationController;
        if (TryGetComponent(out HealthBar hpBar))
            _hpBar = hpBar;
        _attackSpeed = new WaitForSeconds(AttackSpeed);

        if (TryGetComponent(out GreenTeam greenTeam))
        {
            _greenTeam = greenTeam;
            GreenTeamUnitsList.Singleton.GreenTeamList.Add(_greenTeam);
        }
        if (TryGetComponent(out RedTeam redTeam))
        {
            _redTeam = redTeam;
            RedTeamUnitsList.Singleton.RedTeamList.Add(_redTeam);
        }
    }

    virtual protected void OnEnable()
    {
        CurrentHealth = MaxHealth;
        if (_greenTeam != null)
            GreenTeamUnitsList.Singleton.GreenTeamList.Add(_greenTeam);
        else if (_redTeam != null)
            RedTeamUnitsList.Singleton.RedTeamList.Add(_redTeam);
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (!IsTargetSet)
            FindTarget();
    }

    virtual public void UpgradeUnit(int armor, int weapon)
    {
        ArmorUpgrade = armor;
        WeaponUpgrade = weapon;
    }

    private IEnumerator AttackCoroutine()
    {
        while (Target.CurrentHealth > 0)
        {
            if (CurrentHealth > 0 && Vector3.Distance(transform.position, Target.transform.position) <= AttackRange)
            {
                if (_animationController != null)
                    _animationController.AttackTrigger();
                else AttackTarget();
            }

            yield return _attackSpeed;
        }
        if (_animationController != null)
            _animationController.RunTrigger();
        IsTargetSet = false;

    }

    virtual public void AttackTarget()
    {
        Target.GotDamage(Damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, TargetDetectionRadius);
    }

    private void FindTarget()
    {
        if (_greenTeam == null && !IsTargetSet)
            SetTargetFromGenericList(GreenTeamUnitsList.Singleton.GreenTeamList);
        else if (_redTeam == null && !IsTargetSet)
            SetTargetFromGenericList(RedTeamUnitsList.Singleton.RedTeamList);
    }
    private void SetTargetFromGenericList<T>(List<T> list) where T : MonoBehaviour
    {
        foreach (var target in list)
            if (!IsTargetSet && Vector3.Distance(transform.position, target.transform.position) <= TargetDetectionRadius)
            {
                IsTargetSet = true;
                Target = target.GetComponent<UnitParent>();
                StartCoroutine(AttackCoroutine());
            }
    }

    public void GotDamage(int Damage)
    {
        CurrentHealth -= (int)(Random.Range(Damage - 10, Damage) / Armor);
        if (_hpBar != null)
            _hpBar.ChangeHealt();
        if (CurrentHealth <= 0)
            Death();
    }

    private void Death()
    {
        if (_greenTeam != null)
        {
            PlayersEconomy.Singleton.AddMoneyRedTeam(10);
            GreenTeamUnitsList.Singleton.GreenTeamList.Remove(_greenTeam);
        }
        else if (_redTeam != null)
        {
            PlayersEconomy.Singleton.AddMoneyGreenTeam(100);
            RedTeamUnitsList.Singleton.RedTeamList.Remove(_redTeam);
        }

        if (_animationController != null)
            _animationController.DieTrigger();
        StartCoroutine(DeathTimer());
    }

    private IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        IsTargetSet = false;
        Target = null;
    }
}