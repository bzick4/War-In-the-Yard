using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    private GameObject _WeaponCurrent;
    [SerializeField] private Transform _RightHandTransform;
    [SerializeField] private List<WeaponSO> _Weapons;

    private WeaponSO _currentWeaponSO;

    void Update()
    {
        SwitchWeapon();
    }

    private void SwitchWeapon()
    {
         if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentWeaponSO = _Weapons[0];
            ShowCurrentWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentWeaponSO = _Weapons[1];
            ShowCurrentWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentWeaponSO = _Weapons[2];
            ShowCurrentWeapon();
        }
        
    }

    private void ShowCurrentWeapon()
{
    if (_WeaponCurrent != null)
        Destroy(_WeaponCurrent);

    _WeaponCurrent = Instantiate(_currentWeaponSO.PrefabWeapon, _RightHandTransform);
    Debug.Log("оружие " + _currentWeaponSO._WeaponName + " активировано");
}

    
}
