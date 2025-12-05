using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
   [SerializeField] private Item[] _Items;

   private int _itemIndex;
   private int _previousItemIndex = -1;
    private Weapon currentWeapon;

    void Start()
    {
        // Защита на случай пустого массива
        if (_Items == null || _Items.Length == 0) return;

         EquipItem(0);

         // Ищем оружие на активном объекте предмета
        currentWeapon = _Items[_itemIndex].ItemObject.GetComponentInChildren<Weapon>();
        if (currentWeapon != null)
            currentWeapon.enabled = true;
    }

    private void Update()
    {
         if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentWeapon != null)
            {
                currentWeapon.Shoot();
                Debug.Log("Выстрел произведен");
            }
            else
            {
                Debug.LogWarning("Оружие не назначено (currentWeapon == null)");
            }
        }

         ChangeWeapon();
    }   

   private void EquipItem(int index)
   {
       // Защита от пустого массива и выхода за границы
       if (_Items == null || _Items.Length == 0) return;
       if (index < 0 || index >= _Items.Length) return;

       _itemIndex = index;
       _Items[_itemIndex].ItemObject.SetActive(true);

       if(_previousItemIndex != -1 && _previousItemIndex != _itemIndex)
       {
           _Items[_previousItemIndex].ItemObject.SetActive(false);
       }
       _previousItemIndex = _itemIndex;
   }

   private void ChangeWeapon()
    {
         if (Input.GetKeyDown(KeyCode.Alpha1))
         {
              EquipItem(0);
              SwitchWeapon<Sword>();
         }
         else if (Input.GetKeyDown(KeyCode.Alpha2) && _Items.Length > 1)
         {
              EquipItem(1);
              SwitchWeapon<MagicStaff>();
         }
         else if (Input.GetKeyDown(KeyCode.Alpha3) && _Items.Length > 2)
         {
              EquipItem(2);
              SwitchWeapon<MagicStick>();
         }
    }

    private void SwitchWeapon<T>() where T : Weapon
    {
        // Отключаем текущее оружие
        if (currentWeapon != null)
            currentWeapon.enabled = false;
        
        // Ищем новое оружие на активном ItemObject
        if (_Items == null || _Items.Length == 0) {
            currentWeapon = null;
            return;
        }

        var activeItem = _Items[_itemIndex].ItemObject;
        if (activeItem == null) {
            currentWeapon = null;
            Debug.LogWarning("Active item object is null");
            return;
        }

        currentWeapon = activeItem.GetComponentInChildren<T>();
        if (currentWeapon != null)
            currentWeapon.enabled = true;
        else
            Debug.LogWarning("Оружие нужного типа не найдено на активном предмете: " + typeof(T).Name);
    }
}