using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 터렛의 부모 클래스
/// </summary>
public class Turret : MonoBehaviour, IAttackable
{
    [SerializeField] private TurretData _turretData; // 스크립터블 오브젝트 참조
    private ObjectPool _objectPool; // 오브젝트 풀
    [SerializeField] private GameObject _projectilePrefab;  // 발사할 총알 프리팹

    [LabelText("총구 위치")]
    [SerializeField] private Transform _firePoint;          // 발사 위치

    protected virtual void Awake()
    {
        Initialize();
    }
    
    protected virtual void Initialize()
    {
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
