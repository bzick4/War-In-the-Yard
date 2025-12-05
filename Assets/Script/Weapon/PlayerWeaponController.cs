using UnityEngine;

// Система управления оружием игрока
public class PlayerWeaponController : MonoBehaviour
{
    private Weapon currentWeapon;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentWeapon != null)
        {
            currentWeapon.Shoot();
        }
        
        // Смена оружия
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon<Sword>();
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon<MagicStaff>();
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon<MagicStick>();
    }
    
    private void SwitchWeapon<T>() where T : Weapon
    {
        // Отключаем текущее оружие
        if (currentWeapon != null)
            currentWeapon.enabled = false;
        
        // Включаем новое
        currentWeapon = GetComponent<T>();
        if (currentWeapon != null)
            currentWeapon.enabled = true;
    }
}
