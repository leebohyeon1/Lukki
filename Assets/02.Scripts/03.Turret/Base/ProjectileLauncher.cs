using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 터렛 클래스를 상속받아 투사체를 발사하는 터렛 클래스
/// </summary>
public class ProjectileLauncher : Turret, IAttackable
{
    [BoxGroup("전투"),LabelText("총알 풀")]
    [SerializeField] protected BulletPool _pool; // 총알 풀
    [BoxGroup("전투"),LabelText("총알 프리팹")]
    [SerializeField] private GameObject _projectilePrefab;  // 발사할 총알 프리팹
    [BoxGroup("전투"),LabelText("총구 위치")]
    [SerializeField] private Transform _firePoint;          // 발사 위치

    private bool _canAttack = false;    // 공격 가능 여부

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Initialize()
    {
        base.Initialize();

        // 투사체 풀이 없을 경우 새 게임 오브젝트를 생성한다.   
        if (_pool == null)
        {
            _pool = GetComponentInChildren<BulletPool>();
            if (_pool == null)
            {
                GameObject poolObject = new GameObject("BulletPool");
                poolObject.transform.SetParent(transform); // 부모 설정
                _pool = poolObject.AddComponent<BulletPool>();
            }
        }

        if (_projectilePrefab == null)
        {
            Debug.LogError("총알 프리팹이 없습니다.");
            return;
        }

        // 투사체 풀 초기화
        _pool.InitializePool(_installedNumber, _projectilePrefab);

        _canAttack = true;
    }

    /// <summary>
    /// 터렛이 공격하는 함수
    /// 투사체를 풀에서 가져와 전방으로 발사한다.
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
    /// 공격 쿨타임을 코루틴으로 관리한다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(TurretData.FireRate);
        _canAttack = true;
    }

    /// <summary>
    /// 터렛이 특정 방향을 향해 회전하는 함수
    /// </summary>
    /// <param name="Direction"> 터렛이 바라볼 방향 </param>
    public virtual void RotateTurret(Vector3 Direction)
    {

    }
}
