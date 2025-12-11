using UnityEngine;


public class Sword : Weapon
{
    public override void Shoot()
    {
        if (!CanShoot()) return;
        
        Debug.Log("Меч: Ба-бах! Одиночный выстрел");
        PlayTrigger("AttackSword");
        UpdateFireTime();
    }
    public override void AnimShoot()
    {
        // Логика анимации для меча, если требуется
    }
    
}
