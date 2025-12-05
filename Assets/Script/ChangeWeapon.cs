
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField] private List<GameObject> _Weapons;

    public int _currentWeaponIndex;

    void Start()
    {
        UpdateWeaponVisibility();
    }


    void Update()
    {
        ChangeWeapons();
    }

    private void ChangeWeapons()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            _currentWeaponIndex = (_currentWeaponIndex + 1) % _Weapons.Count;
            UpdateWeaponVisibility();
        }
        else if (scroll < 0f)
        {
            _currentWeaponIndex--;
            if (_currentWeaponIndex < 0) _currentWeaponIndex = _Weapons.Count - 1;
            UpdateWeaponVisibility();
        }
    }

    private void UpdateWeaponVisibility()
    {
        for (int i = 0; i < _Weapons.Count; i++)
        {
            _Weapons[i].SetActive(i == _currentWeaponIndex);
        }
    }
}
