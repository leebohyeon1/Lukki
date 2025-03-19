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

    private float _smoothSteering = 0f;

    private void Start()
    {
        if(!_engine || !_wheel)
        {
            Debug.LogError("Engine �Ǵ� Wheel ������Ʈ �� ����");
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
