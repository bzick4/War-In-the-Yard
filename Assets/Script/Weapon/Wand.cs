using UnityEngine;


public class Wand : Weapon
{
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int burstCount = 3;

    private bool isBursting = false;

    public override void Shoot()
    {
        if (!CanShoot() || isBursting) return;
        
        Debug.Log("Палочка: Атакуем! Анимация запускает очередь.");
        PlayTrigger("AttackStick");
        UpdateFireTime();
    }

    public override void AnimShoot()
    {
        if (!isBursting)
            StartCoroutine(BurstFire());
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
