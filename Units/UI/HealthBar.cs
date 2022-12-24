using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private GameObject _healthBar;
    private Quaternion _startRotation;
    private UnitParent _unit;
    private float _health;

    private void Awake()
    {
        _startRotation = _healthBar.transform.rotation;
        _unit = GetComponent<UnitParent>();        
    }
    private void OnEnable()
    {
        _health = _unit.MaxHealth;
        _healthBarImage.fillAmount = _health;
        _healthBar.SetActive(true);
    }

    private void LateUpdate()
    {
        _healthBar.transform.rotation = _startRotation;
        //_healthBar.transform.position =  new Vector3(_unit.transform.position.x, 2.5f, _unit.transform.position.z);
    }

    public void ChangeHealt()
    {
        _healthBarImage.fillAmount = _unit.CurrentHealth / _health;
        if (_unit.CurrentHealth <= 0)
            _healthBar.SetActive(false);
    }
}