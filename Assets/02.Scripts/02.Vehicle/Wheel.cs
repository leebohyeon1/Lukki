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
    [SerializeField] private float _maxSteeringAngle = 40f;


    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }
    public void Steer(float input)
    {
        float speedFactor = Mathf.Clamp(_rb.velocity.magnitude / 50f, 0.6f, 1f);
        float steerAngle = _maxSteeringAngle * input * speedFactor;

        _frontLeftWheel.steerAngle = steerAngle;
        _frontRightWheel.steerAngle = steerAngle;
    }

    public void ResetSteering()
    {
        _frontLeftWheel.steerAngle = Mathf.Lerp(_frontLeftWheel.steerAngle, 0, Time.fixedDeltaTime * 5f);
        _frontRightWheel.steerAngle = Mathf.Lerp(_frontRightWheel.steerAngle, 0, Time.fixedDeltaTime * 5f);
    }

    public void AdjustSuspension()
    {
        JointSpring spring = new JointSpring
        {
            spring = 35000f,
            damper = 4500f,
            targetPosition = 0.5f
        };

        _frontLeftWheel.suspensionSpring = spring;
        _frontRightWheel.suspensionSpring = spring;
        _rearLeftWheel.suspensionSpring = spring;
        _rearRightWheel.suspensionSpring = spring;
    }
}
