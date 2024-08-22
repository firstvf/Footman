using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneUnit : UnitParent
{
    public override float TargetDetectionRadius { get; protected set; }
    public override float AttackRange { get; protected set; }
    public override float AttackSpeed { get; protected set; }
    public override float Armor { get; protected set; }
    public override int CurrentHealth { get; protected set; }
    public override int Damage { get; protected set; }
    public override int MaxHealth { get ; protected set; }
    public override int WeaponUpgrade { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }
    public override int ArmorUpgrade { get => throw new System.NotImplementedException(); protected set => throw new System.NotImplementedException(); }

    [SerializeField] private GameObject _arrow;

    protected override void Awake()
    {
        MaxHealth = 500;
        Armor = 1;
        Damage = 25;
        TargetDetectionRadius = 4f;
        AttackRange = 1.5f;
        AttackSpeed = 2f;

        base.Awake();
    }

    public override void AttackTarget()
    {
        //instantiate from object pool
        base.AttackTarget();
    }
}