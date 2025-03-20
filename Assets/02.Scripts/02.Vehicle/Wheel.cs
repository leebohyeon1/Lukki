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
    [SerializeField] private float _maxSteeringAngle =20f;


    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }
    public void Steer(float input)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,-transform.up,out hit,2f))
        {
            Vector3 groundNormal= hit.normal;
            float slopeAngle = Vector3.Angle(groundNormal, Vector3.up);


            float speedFactor = Mathf.Clamp(1 - (_rb.velocity.magnitude / 80f), 0.3f, 1f);
            float slopeFactor = Mathf.Clamp(1 - (slopeAngle / 45f), 0.5f, 1f);

            float steerAngle = _maxSteeringAngle * input * speedFactor;

            _frontLeftWheel.steerAngle = steerAngle;
            _frontRightWheel.steerAngle = steerAngle;
        }
        
    }

    public void ResetSteering()
    {
        _frontLeftWheel.steerAngle = Mathf.Lerp(_frontLeftWheel.steerAngle, 0, Time.fixedDeltaTime * 5f);
        _frontRightWheel.steerAngle = Mathf.Lerp(_frontRightWheel.steerAngle, 0, Time.fixedDeltaTime * 5f);
    }

    public void AdjustSuspension()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
        {
            // ������ ������ ���󰡵��� ����
            Vector3 groundNormal = hit.normal;
            Quaternion groundRotation = Quaternion.FromToRotation(transform.up, groundNormal);
            transform.rotation = Quaternion.Lerp(transform.rotation, groundRotation * transform.rotation, Time.fixedDeltaTime * 5f);
        }

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
