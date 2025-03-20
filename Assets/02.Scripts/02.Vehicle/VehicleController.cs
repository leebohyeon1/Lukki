using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class VehicleController : MonoBehaviour
{
    [FoldoutGroup("�����ұ�")]
    [SerializeField] private Engine _engine;
    [FoldoutGroup("�����ұ�")]
    [SerializeField] private Wheel _wheel;

    [FoldoutGroup("���� ��ü���ͽ�"), ReadOnly]
    [SerializeField] private Vector2 _moveInput;
    [FoldoutGroup("���� ��ü���ͽ�"), ReadOnly]
    [SerializeField] private bool _isBraking;
    private Rigidbody _rb;

    private float _smoothSteering = 0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if(!_engine || !_wheel)
        {
            Debug.LogError("Engine �Ǵ� Wheel ������Ʈ �� ����");
        }

        _rb.centerOfMass = new Vector3(0, -1.5f, 0); // ������ �������� ���ؼ� �����߽��� ���� �ʿ��  Y�� ����
    }

    public void SetInput(Vector2 input, bool brake)
    {
        _moveInput = input;
        _isBraking = brake;
    }

    
    private void FixedUpdate()
    {
        float accel = _moveInput.y * 0.5f;
        float steering = _moveInput.x;

        _smoothSteering = Mathf.Lerp(_smoothSteering, steering, Time.fixedDeltaTime * 3f);
        if(accel != 0)
        {
            _engine.Accelerate(accel);
        }
        if(_isBraking)
        {
            _engine.Brake(_isBraking);
        }
        if(steering != 0)
        {
            _wheel.Steer(_smoothSteering);
        }
        else
        {
            _wheel.ResetSteering();
        }
    }

    private bool IsGrounded()
    {
        return Physics.
    }

}
