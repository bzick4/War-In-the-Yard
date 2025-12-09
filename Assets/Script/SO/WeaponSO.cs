using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/WeaponInfo")]
public class WeaponSO : ScriptableObject
{

    public GameObject PrefabWeapon;
    public string _WeaponName;
    public float _Damage;
    public float _StaminaCost;
    public float _ManaCost;

    
}

