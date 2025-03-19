using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 게임에서 조종하는 터렛 클래스
/// 플레이어의 제일 기본 무기이다.
/// </summary>
public class PlayerTurret : ProjectileLauncher
{
    private Transform _cameraTransform; // 카메라의 Transform을 저장할 변수
    private Transform _headTransform;

    void Start()
    {
        _cameraTransform = Camera.main.transform;
        _headTransform =  transform.GetChild(0);
    }

    private  void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Attack();
        }

        RotateTurret();
    }

    protected override void Initialize()
    {
        _installedNumber = 0;

        base.Initialize();
    }

    public override void Attack()
    {
        base.Attack();
    }

    // 터렛을 카메라 방향으로 회전시키는 메서드
    private void RotateTurret()
    {
        if (_cameraTransform == null)
        {
            Debug.LogError("카메라가 설정되지 않았습니다.");
            return;
        }

        // 카메라의 forward 벡터를 수평면에 투영
        Vector3 cameraForward = _cameraTransform.forward;
        cameraForward.Normalize(); // 벡터를 정규화

        // 터렛의 회전을 카메라 방향으로 설정
        if (cameraForward != Vector3.zero)
        {
            _headTransform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }

    public override void RotateTurret(Vector3 Direction)
    {
        
    }
}
