using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 총알의 부모 클래스
/// 총알은 풀링가능한 오브젝트이기에 PoolingObject를 상속받는다.
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
    /// 이 오브젝트가 귀속될 풀을 지정하는 메서드
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
            // 만약 풀 정보가 없다면 그냥 파괴
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
