using UnityEngine;


public class MagicStick : Weapon
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int burstCount = 3;
    private bool isBursting = false;
    
    public override void Shoot()
    {
        if (!CanShoot() || isBursting) return;
        
        Debug.Log("Палочка: Тра-та-та! Очередь выстрелов");
        PlayTrigger("AttackStick");
        StartCoroutine(BurstFire());
        UpdateFireTime();
    }
    
    private System.Collections.IEnumerator BurstFire()
    {
        isBursting = true;
        
        for (int i = 0; i < burstCount; i++)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            yield return new WaitForSeconds(0.1f);
        }
        
        isBursting = false;
    }
}
