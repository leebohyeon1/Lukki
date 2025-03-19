using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �÷��̾ ���ӿ��� �����ϴ� �ͷ� Ŭ����
/// �÷��̾��� ���� �⺻ �����̴�.
/// </summary>
public class PlayerTurret : ProjectileLauncher
{
    private Transform _cameraTransform; // ī�޶��� Transform�� ������ ����
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

    // �ͷ��� ī�޶� �������� ȸ����Ű�� �޼���
    private void RotateTurret()
    {
        if (_cameraTransform == null)
        {
            Debug.LogError("ī�޶� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ī�޶��� forward ���͸� ����鿡 ����
        Vector3 cameraForward = _cameraTransform.forward;
        cameraForward.Normalize(); // ���͸� ����ȭ

        // �ͷ��� ȸ���� ī�޶� �������� ����
        if (cameraForward != Vector3.zero)
        {
            _headTransform.rotation = Quaternion.LookRotation(cameraForward);
        }
    }

    public override void RotateTurret(Vector3 Direction)
    {
        
    }
}
