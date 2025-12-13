using System.Collections;
using UnityEngine;

public class Wand : Weapon
{
    [SerializeField] private Transform _BulletSpawnPoint;
    [SerializeField] private string bulletPrefabResourcePath = "Prefabs/Bullet";
    [SerializeField] private float _BulletSpeed = 10f;
    [SerializeField] private int burstCount = 3;

    private GameObject _bulletPrefab;
    private BulletPool _bulletPool;
    private bool isBursting = false;

    protected override void Awake()
    {
        base.Awake();
        

        _bulletPrefab = Resources.Load<GameObject>(bulletPrefabResourcePath);

        if (_bulletPrefab == null)
            Debug.LogError($"Wand: не найден префаб пули в Resources/{bulletPrefabResourcePath}");
    }

    private bool EnsurePool()
    {
        if (_bulletPool == null)
            _bulletPool = FindObjectOfType<BulletPool>();

        return _bulletPool != null;
    }

    public override void Shoot()
    {
        if (!CanShoot() || isBursting) return;

        UseManna();
    }

    public override void AnimShoot()
    {
        if (!EnsurePool())
        {
            Debug.LogError("Wand: BulletPool не найден в сцене!");
            return;
        }

        if (!isBursting)
            StartCoroutine(BurstFire());
    }

    
    private void UseManna(float manaCostPerShot=10f)
    {
        if (_dataView.CurrentManna >= manaCostPerShot)
            {
                PlayTrigger("AttackStick");
                _dataView.UseManna(manaCostPerShot);
                UpdateFireTime();
            }
            else
            {
               Debug.Log("Wand: недостаточно маны для выстрела!");
            }
    }

    private void FireOneBullet()
    {
        GameObject bulletGO = _bulletPool.GetObject();
        bulletGO.transform.position = _BulletSpawnPoint.position;
        bulletGO.transform.rotation = _BulletSpawnPoint.rotation;

        Rigidbody rb = bulletGO.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = _BulletSpawnPoint.right * _BulletSpeed;

        
        StartCoroutine(ReturnAfterDelay(bulletGO, 4f));
    }

    private IEnumerator BurstFire()
    {
        isBursting = true;

        FireOneBullet();

        yield return new WaitForSeconds(0.1f);

        isBursting = false;
    }

    private IEnumerator ReturnAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        _bulletPool.ReturnObject(bullet);
    }
}