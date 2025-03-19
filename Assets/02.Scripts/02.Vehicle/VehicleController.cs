using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class VehicleController : MonoBehaviour
{
    [FoldoutGroup("뭐로할까")]
    [SerializeField] private Engine _engine;
    [FoldoutGroup("뭐로할까")]
    [SerializeField] private Wheel _wheel;

    [FoldoutGroup("차량 스체이터스"), ReadOnly]
    [SerializeField] private Vector2 _moveInput;
    [FoldoutGroup("차량 스체이터스"), ReadOnly]
    [SerializeField] private bool _isBraking;
    private Rigidbody _rb;

    private float _smoothSteering = 0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if(!_engine || !_wheel)
        {
            Debug.LogError("Engine 또는 Wheel 컴포넌트 가 없음");
        }

        _rb.centerOfMass = new Vector3(0, -2.0f, 0); // 차량의 안정성을 위해서 무게중심을 낮춤 필요시  Y값 변경
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

        StablizeSteering();

        if(Mathf.Abs(steering)>0.1f)
        {
            _wheel.Steer(_smoothSteering);
        }
        else
        {
            _wheel.ResetSteering();
        }
        
    }

    // 지면의 경사에 따른 조향각 안정화
    public void StablizeSteering()
    {
        RaycastHit hitFront, hitRear; // 앞 뒤로 레이를 두개를 사용하여 평균 기울기를 계산하여 안정화에 사용

        Vector3 frontRayPos = transform.position + transform.forward * 1.5f;
        Vector3 rearRayPos = transform.position - transform.forward * 1.5f;

        bool hasFrontHit = Physics.Raycast(frontRayPos, Vector3.down, out hitFront, 2.0f);
        bool hasRearHit = Physics.Raycast(rearRayPos, Vector3.down, out hitRear, 2.0f);

        if(hasFrontHit && hasRearHit)
        {
            float slopeAngleFront = Vector3.Angle(hitFront.normal, Vector3.up);
            float slopeAngleRear= Vector3.Angle(hitRear.normal, Vector3.up);
            float avgSlopeAngle = (slopeAngleFront + slopeAngleRear) * 0.5f;

            if(avgSlopeAngle >10f)
            {
                _smoothSteering *= 0.7f;
            }
        }
    }
}
