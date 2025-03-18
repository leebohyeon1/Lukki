using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : Turret, IAttackable
{
    [BoxGroup("전투"),LabelText("오브젝트 풀")]
    [SerializeField] private ObjectPool _objectPool; // 오브젝트 풀
    [BoxGroup("전투"),LabelText("총알 프리팹")]
    [SerializeField] private GameObject _projectilePrefab;  // 발사할 총알 프리팹
    [BoxGroup("전투"),LabelText("총구 위치")]
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
            if (_objectPool == null)
            {
                GameObject poolObject = new GameObject("ObjectPool");
                poolObject.transform.SetParent(transform); // 부모 설정
                _objectPool = poolObject.AddComponent<ObjectPool>();
            }
        }

        if (_projectilePrefab == null)
        {
            Debug.LogError("총알 프리팹이 없습니다.");
            return;
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
