using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ͷ��� �θ� Ŭ����
/// </summary>
public class Turret : MonoBehaviour, IAttackable
{
    [SerializeField] private TurretData _turretData; // ��ũ���ͺ� ������Ʈ ����
    private ObjectPool _objectPool; // ������Ʈ Ǯ
    [SerializeField] private GameObject _projectilePrefab;  // �߻��� �Ѿ� ������

    [LabelText("�ѱ� ��ġ")]
    [SerializeField] private Transform _firePoint;          // �߻� ��ġ

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
