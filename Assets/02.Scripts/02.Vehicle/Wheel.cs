using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// - 바퀴시스템
///     - 조향(steer)
///     - 마찰력 관리
///     - 서스펜션 
/// </summary>
public class Wheel : MonoBehaviour
{
    [FoldoutGroup("휠 콜리더")]
    [SerializeField] private WheelCollider _frontLeftWheel;
    [FoldoutGroup("휠 콜리더")]
    [SerializeField] private WheelCollider _frontRightWheel;
    [FoldoutGroup("휠 콜리더")]
    [SerializeField] private WheelCollider _rearLeftWheel;
    [FoldoutGroup("휠 콜리더")]
    [SerializeField] private WheelCollider _rearRightWheel;

    [FoldoutGroup("휠 세팅")]
    [SerializeField] private float _maxSteeringAngle =25f; // 최대조향각
    [FoldoutGroup("휠 세팅")]
    [SerializeField] private float _steeringDamping = 3f; //조향 복귀 감속 조절


    private Rigidbody _rb;
    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
    }
    public void Steer(float input)
    {
        float steerAngle = _maxSteeringAngle * input; // 기본 조향각 설정

        float directionFactor = Vector3.Dot(_rb.velocity.normalized, transform.forward); //차량 진행방향 감지 (전진/후진)


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
