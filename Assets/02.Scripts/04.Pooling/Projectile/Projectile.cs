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
    private Rigidbody _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); 
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
    public void SetPool(BulletPool pool)
    {
        _pool = pool;
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

            _pool.ReturnBullet(gameObject);
        }
    }
    public void SetVelocity(Vector3 direction, float speed)
    {
        _rigidbody.velocity = direction * speed;
    }

}
