using UnityEngine;


public class MagicStaff : Weapon
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int pelletCount = 5;
    [SerializeField] private float spreadAngle = 30f;
    
    public override void Shoot()
    {
        if (!CanShoot()) return;
        PlayTrigger("AttackStaff");
        
        Debug.Log($"посох: БУМ! Выстрел {pelletCount} пулями веером");
        
        for (int i = 0; i < pelletCount; i++)
        {
            float angle = spreadAngle * (i / (float)(pelletCount - 1) - 0.5f);
            Quaternion rotation = bulletSpawnPoint.rotation * Quaternion.Euler(0, angle, 0);
            Instantiate(bulletPrefab, bulletSpawnPoint.position, rotation);
        }
        
        UpdateFireTime();
    }
}