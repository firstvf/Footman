public class SwordMan : UnitParent
{
    public override float TargetDetectionRadius { get; protected set; }
    public override float AttackSpeed { get; protected set; }
    public override float AttackRange { get; protected set; }
    public override float Armor { get; protected set; }
    public override int CurrentHealth { get; protected set; }
    public override int WeaponUpgrade { get; protected set; }
    public override int ArmorUpgrade { get; protected set; }
    public override int MaxHealth { get; protected set; }
    public override int Damage { get; protected set; }

    private int _currentWeponLevel;
    private int _currentArmorLevel;
    private bool _isEquipChange;

    override protected void Awake()
    {
        MaxHealth = 70;
        Armor = 1;
        Damage = 25;
        TargetDetectionRadius = 4f;
        AttackRange = 1.5f;
        AttackSpeed = 2f;
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (WeaponUpgrade > _currentWeponLevel || ArmorUpgrade > _currentArmorLevel)
            UpgradeSwordMan();
    }

    private void UpgradeSwordMan()
    {
        if (ArmorUpgrade > _currentArmorLevel)
        {
            for (int i = _currentArmorLevel; i < ArmorUpgrade; i++)
            {
                MaxHealth += 10;
                Armor += 0.5f;
            }
            _currentArmorLevel = ArmorUpgrade;
            _isEquipChange = true;
        }

        if (WeaponUpgrade > _currentWeponLevel)
        {
            for (int i = _currentWeponLevel; i < WeaponUpgrade; i++)
                Damage += 10;

            _currentWeponLevel = WeaponUpgrade;
            _isEquipChange = true;
        }

        if (_isEquipChange)
        {
            GetComponent<SwordManUpgrade>().UpgradeSwordMan(_currentWeponLevel, _currentArmorLevel);
            _isEquipChange = false;
        }
    }
}