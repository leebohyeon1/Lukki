using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : Turret, IAttackable
{
    [SerializeField] private ObjectPool _objectPool; // 오브젝트 풀
    [SerializeField] private GameObject _projectilePrefab;  // 발사할 총알 프리팹
    [LabelText("총구 위치")]
    [SerializeField] private Transform _firePoint;          // 발사 위치

    protected override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Initialize()
    {
        base.Initialize();

        if (_objectPool == null)
        {
            _objectPool = GetComponentInChildren<ObjectPool>();
        }

        _objectPool.InitializePool(_projectilePrefab);
    }

    [Button]
    public virtual void Attack()
    {
        GameObject bullet = _objectPool.GetBullet();
        bullet.transform.position = _firePoint.position;
        bullet.transform.rotation = _firePoint.rotation;
    }
}
