using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ѿ��� �θ� Ŭ����
/// �Ѿ��� Ǯ�������� ������Ʈ�̱⿡ PoolingObject�� ��ӹ޴´�.
/// </summary>
public class Projectile : MonoBehaviour
{
    private BulletPool _pool;
    [SerializeField] private Rigidbody _rigidbody;

    private int _turretInstalledNumber;

    private void Awake()
    {
        if (_rigidbody == null)
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        ReturnToPool();
    }

    /// <summary>
    /// �� ������Ʈ�� �ͼӵ� Ǯ�� �����ϴ� �޼���
    /// </summary>
    public void SetPool(BulletPool pool, int installedNumber)
    {
        _pool = pool;
        _turretInstalledNumber = installedNumber;
    }

    protected void ReturnToPool()
    {
        if (_pool == null)
        {
            // ���� Ǯ ������ ���ٸ� �׳� �ı�
            Destroy(gameObject);
        }
        else
        {

            _pool.ReturnBullet(_turretInstalledNumber, gameObject);
        }
    }

    public void SetVelocity(Vector3 direction, float speed)
    {
        _rigidbody.velocity = direction * speed;
    }

}
