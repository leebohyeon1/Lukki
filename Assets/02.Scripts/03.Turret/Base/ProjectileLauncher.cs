using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ͷ� Ŭ������ ��ӹ޾� ����ü�� �߻��ϴ� �ͷ� Ŭ����
/// </summary>
public class ProjectileLauncher : Turret, IAttackable
{
    [BoxGroup("����"),LabelText("�Ѿ� Ǯ")]
    [SerializeField] protected BulletPool _pool; // �Ѿ� Ǯ
    [BoxGroup("����"),LabelText("�Ѿ� ������")]
    [SerializeField] private GameObject _projectilePrefab;  // �߻��� �Ѿ� ������
    [BoxGroup("����"),LabelText("�ѱ� ��ġ")]
    [SerializeField] private Transform _firePoint;          // �߻� ��ġ

    private bool _canAttack = false;    // ���� ���� ����

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Initialize()
    {
        base.Initialize();

        // ����ü Ǯ�� ���� ��� �� ���� ������Ʈ�� �����Ѵ�.   
        if (_pool == null)
        {
            _pool = GetComponentInChildren<BulletPool>();
            if (_pool == null)
            {
                GameObject poolObject = new GameObject("BulletPool");
                poolObject.transform.SetParent(transform); // �θ� ����
                _pool = poolObject.AddComponent<BulletPool>();
            }
        }

        if (_projectilePrefab == null)
        {
            Debug.LogError("�Ѿ� �������� �����ϴ�.");
            return;
        }

        // ����ü Ǯ �ʱ�ȭ
        _pool.InitializePool(_installedNumber, _projectilePrefab);

        _canAttack = true;
    }

    /// <summary>
    /// �ͷ��� �����ϴ� �Լ�
    /// ����ü�� Ǯ���� ������ �������� �߻��Ѵ�.
    /// </summary>
    public virtual void Attack()
    {
        if(_canAttack)
        {
            GameObject bullet = _pool.GetBullet(_installedNumber);
            bullet.transform.position = _firePoint.position;
            bullet.transform.rotation = _firePoint.rotation;

            Projectile projectile = bullet.GetComponent<Projectile>();
            if (projectile != null)
            {
                projectile.SetVelocity(_firePoint.forward.normalized, TurretData.ProjectileSpeed);
            }

            _canAttack = false;
            StartCoroutine(ReloadCoroutine());
        }    
    }

    /// <summary>
    /// ���� ��Ÿ���� �ڷ�ƾ���� �����Ѵ�.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(TurretData.FireRate);
        _canAttack = true;
    }

    /// <summary>
    /// �ͷ��� Ư�� ������ ���� ȸ���ϴ� �Լ�
    /// </summary>
    /// <param name="Direction"> �ͷ��� �ٶ� ���� </param>
    public virtual void RotateTurret(Vector3 Direction)
    {

    }
}
