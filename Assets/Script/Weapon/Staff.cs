// using System.Collections;
// using UnityEngine;


// public class Staff : Weapon
// {
//     [SerializeField] private Transform _BulletSpawnPoint;
//     [SerializeField] private string bulletPrefabResourcePath = "Prefabs/Bullet";
//     [SerializeField] private float _BulletSpeed=10;
    

//     [Header("Насколько пуль и разброс")]
//     [SerializeField] private int pelletCount = 5;
//     [SerializeField] private float spreadAngle = 3;
    
//     private GameObject _bulletPrefab;
//     private BulletPool _bulletPool;


//    protected override void Awake()
//     {
//         base.Awake();

//         _bulletPool = FindObjectOfType<BulletPool>();

//         if (_bulletPrefab == null)
//             _bulletPrefab = Resources.Load<GameObject>(bulletPrefabResourcePath);

//         if (_bulletPrefab == null)
//             Debug.LogError($"Wand: не найден префаб пули в Resources/{bulletPrefabResourcePath}");
//     }

//     public override void Shoot()
//     {
//         if (!CanShoot()) return;
//         PlayTrigger("AttackStaff");
        
//         Debug.Log($"посох: БУМ! Выстрел {pelletCount} пулями веером");
        
//         UpdateFireTime();
//     }

//      private void ActiveShoot()
//     {
//         Rigidbody bulletRB =_bulletPrefab.GetComponent<Rigidbody>();

//         if(bulletRB != null)
//         {
//             bulletRB.linearVelocity = _BulletSpawnPoint.forward * _BulletSpeed;
//         }
        
        
//     }

//     public override void AnimShoot()
//     {
//         if (_bulletPrefab == null) return;

//         int count = Mathf.Max(1, pelletCount);

//         for (int i = 0; i < count; i++)
//         {
//             ActiveShoot();
            
//             float t = (count == 1) ? 0.5f : i / (float)(count - 1);

//             // Смещение угла (веер)
//             float angle = spreadAngle * (t - 0.5f);

//             Quaternion rotation =
//                 _BulletSpawnPoint.rotation *
//                 Quaternion.Euler(0, angle, 0);

//                 GameObject bullet = _bulletPool.GetObject();

//                 StartCoroutine(ReturnBullet(bullet, 4f));

//             //Instantiate(_bulletPrefab, _BulletSpawnPoint.position, rotation);
//         }

        
//     }

//     private IEnumerator ReturnBullet(GameObject bullet, float delay)
//     {
//         yield return new WaitForSeconds(delay);
//         _bulletPool.ReturnObject(bullet);
        
//     }
// }
using System.Collections;
using UnityEngine;

public class Staff : Weapon
{
    [SerializeField] private Transform _BulletSpawnPoint;
    [SerializeField] private string bulletPrefabResourcePath = "Prefabs/Bullet";
    [SerializeField] private float _BulletSpeed = 30f;

    [Header("Дробовик настройки")]
    [SerializeField] private int pelletCount = 5;
    [SerializeField] private float spreadAngle = 10f; // градусов (шире -> сильнее разброс)

    private GameObject _bulletPrefab;
    private BulletPool _bulletPool;
    private bool isBursting = false;

    protected override void Awake()
    {
        base.Awake();

        _bulletPrefab = Resources.Load<GameObject>(bulletPrefabResourcePath);

        if (_bulletPrefab == null)
            Debug.LogWarning("Префаб пули не найден: " + bulletPrefabResourcePath);

        // попробуем сразу получить пул, но AnimShoot также делает EnsurePool()
        _bulletPool = FindObjectOfType<BulletPool>();
    }

    private bool EnsurePool()
    {
        if (_bulletPool == null)
            _bulletPool = FindObjectOfType<BulletPool>();
        return _bulletPool != null;
    }

    public override void Shoot()
    {
        if (!CanShoot()) return;

        PlayTrigger("AttackStaff");
        Debug.Log($"посох: дробовой выстрел {pelletCount} пуль");
        UpdateFireTime();
    }

    public override void AnimShoot()
    {
        if (_BulletSpawnPoint == null)
        {
            Debug.LogError("Staff: _BulletSpawnPoint не задан!");
            return;
        }

        if (!EnsurePool())
        {
            Debug.LogError("Staff: BulletPool не найден в сцене!");
            return;
        }

        if (!isBursting)
            StartCoroutine(BurstFire());
    }

    private void FireOneBurst()
    {
        int count = Mathf.Max(1, pelletCount);

        for (int i = 0; i < count; i++)
        {
            // случайный разброс в пределах spreadAngle/2 влево/вправо
            float half = spreadAngle * 0.5f;
            float angle = Random.Range(-half, half);

            Quaternion rotation = _BulletSpawnPoint.rotation * Quaternion.Euler(0f, angle, 0f);

            GameObject bulletGO = _bulletPool.GetObject();
            bulletGO.transform.position = _BulletSpawnPoint.position;
            bulletGO.transform.rotation = rotation;

            Rigidbody rb = bulletGO.GetComponent<Rigidbody>();
            if (rb != null)
                rb.linearVelocity = rotation * -_BulletSpawnPoint.right * _BulletSpeed;
//rotation * Vector3.right * _BulletSpeed;
            

            StartCoroutine(ReturnAfterDelay(bulletGO, 4f));
        }
    }

    private IEnumerator BurstFire()
    {
        isBursting = true;

        FireOneBurst(); // всё выстрелы одной волной

        yield return new WaitForSeconds(0.15f); // задержка между возможными "забросами"
        isBursting = false;
    }

    private IEnumerator ReturnAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (_bulletPool != null)
            _bulletPool.ReturnObject(bullet);
        else
            Destroy(bullet);
    }
}