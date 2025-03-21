using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// - �����ý���
///     - ����(steer)
///     - ������ ����
///     - ������� 
/// </summary>
public class Wheel : MonoBehaviour
{
    [FoldoutGroup("�� �ݸ���")]
    [SerializeField] private WheelCollider _frontLeftWheel;
    [FoldoutGroup("�� �ݸ���")]
    [SerializeField] private WheelCollider _frontRightWheel;
    [FoldoutGroup("�� �ݸ���")]
    [SerializeField] private WheelCollider _rearLeftWheel;
    [FoldoutGroup("�� �ݸ���")]
    [SerializeField] private WheelCollider _rearRightWheel;

    [FoldoutGroup("�� ����")]
    [SerializeField] private float _maxSteeringAngle =25f; // �ִ����Ⱒ
    [FoldoutGroup("�� ����")]
    [SerializeField] private float _steeringDamping = 3f; //���� ���� ���� ����


    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }
    public void Steer(float input)
    {
        float steerAngle = _maxSteeringAngle * input; // �⺻ ���Ⱒ ����

        float directionFactor = Vector3.Dot(_rb.velocity.normalized, transform.forward); //���� ������� ���� (����/����)


        if(Mathf.Abs(directionFactor) < -0.1f && Mathf.Abs(input) <0.01f)
        {
            ResetSteering();
            return;
        }

        float speedFactor = Mathf.Clamp(1 - (_rb.velocity.magnitude / 100f), 0.7f, 1f);
        steerAngle *= speedFactor;

        if(Mathf.Abs(input) < 0.01f)
        {
            _frontLeftWheel.steerAngle = Mathf.Lerp(_frontLeftWheel.steerAngle, 0, Time.fixedDeltaTime * _steeringDamping);
            _frontRightWheel.steerAngle = Mathf.Lerp(_frontRightWheel.steerAngle, 0, Time.fixedDeltaTime * _steeringDamping);
        }
       

            _frontLeftWheel.steerAngle = steerAngle;
            _frontRightWheel.steerAngle = steerAngle;
        
        
    }

    public void ResetSteering()
    {
        _frontLeftWheel.steerAngle = Mathf.Lerp(_frontLeftWheel.steerAngle, 0, Time.fixedDeltaTime * _steeringDamping);
        _frontRightWheel.steerAngle = Mathf.Lerp(_frontRightWheel.steerAngle, 0, Time.fixedDeltaTime * _steeringDamping);
    }

    
}
