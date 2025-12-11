using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    private Weapon _currentWeapon => GetComponentInChildren<Weapon>();

    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.F))
        {
            if (_currentWeapon != null)
            {
                _currentWeapon.Shoot();
                Debug.Log("Выстрел произведен");
            }
            else
            {
                Debug.LogWarning("Оружие не назначено " + _currentWeapon);
            }
        }
    }   

    public void AnimShoot()
    {
        _currentWeapon.AnimShoot();
    }

   

   
}