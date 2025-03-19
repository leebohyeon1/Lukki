using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// - ¹ÙÄû½Ã½ºÅÛ
///     - Á¶Çâ(steer)
///     - ¸¶Âû·Â °ü¸®
///     - ¼­½ºÆæ¼Ç 
/// </summary>
public class Wheel : MonoBehaviour
{
    [FoldoutGroup("ÈÙ ÄÝ¸®´õ")]
    [SerializeField] private WheelCollider _frontLeftWheel;
    [FoldoutGroup("ÈÙ ÄÝ¸®´õ")]
    [SerializeField] private WheelCollider _frontRightWheel;
    [FoldoutGroup("ÈÙ ÄÝ¸®´õ")]
    [SerializeField] private WheelCollider _rearLeftWheel;
    [FoldoutGroup("ÈÙ ÄÝ¸®´õ")]
    [SerializeField] private WheelCollider _rearRightWheel;

    [FoldoutGroup("ÈÙ ¼¼ÆÃ")]
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

    public void AdjustFriction()
    {
        WheelFrictionCurve forwardFriction = _frontLeftWheel.forwardFriction;
        forwardFriction.stiffness = 1.8f;
        _frontLeftWheel.forwardFriction = forwardFriction;
        _frontRightWheel.forwardFriction = forwardFriction;
        _rearLeftWheel.forwardFriction = forwardFriction;
        _rearRightWheel.forwardFriction = forwardFriction;

        WheelFrictionCurve sidewaysFriction = _frontLeftWheel.sidewaysFriction;
        sidewaysFriction.stiffness = 1.7f; //
        _frontLeftWheel.sidewaysFriction = sidewaysFriction;
        _frontRightWheel.sidewaysFriction = sidewaysFriction;
        _rearLeftWheel.sidewaysFriction = sidewaysFriction;
        _rearRightWheel.sidewaysFriction = sidewaysFriction;
    }
}
