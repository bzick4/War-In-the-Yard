using UnityEngine;


public class Sword : Weapon
{

    public override void Shoot()
    {
        if (!CanShoot()) return;
        
        UseStamina();
        
    }
    public override void AnimShoot()
    {
        // Логика анимации для меча, если требуется
    }

    private void UseStamina(float staminaCostPerHit=15f)
    {
            if (_dataView.CurrentStamina >= staminaCostPerHit)
            {
                Debug.Log("Меч: Ба-бах! Одиночный выстрел");
                PlayTrigger("AttackSword");
                _dataView.UseStamina(staminaCostPerHit);
                UpdateFireTime();
            }
            else
            {
                Debug.Log("Sword: недостаточно стамины для удара!");
            }
    }
    
}
