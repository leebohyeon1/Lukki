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

    private float _smoothSteering = 0f;

    private void Start()
    {
        if(!_engine || !_wheel)
        {
            Debug.LogError("Engine 또는 Wheel 컴포넌트 가 없음");
        }
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
}
