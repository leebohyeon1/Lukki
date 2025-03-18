using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Engine : MonoBehaviour
{
    [FoldoutGroup("엔진세팅")]
    [SerializeField] private float _maxTorque = 2500f;// 엔진 토크 (엔진 힘이라 생각하면됨
    [FoldoutGroup("엔진세팅")]
    [SerializeField] private float _brakeForce = 2000f; // 브레이크 힘
    [FoldoutGroup("엔진세팅")]
    [SerializeField] private float _accelerationSpeed = 5f;
    [FoldoutGroup("엔진세팅")]
    [SerializeField] private float _decelerationSpeed = 3f;
    [FoldoutGroup("엔진세팅")]
    [SerializeField] private float _maxSpeed = 40f;

    private Rigidbody _rb;
    private float _currentSpeed = 0f;
    
    void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody를 찾을 수 없습니다 Car 오브젝트를 확인하세요.");
        }
        else
        {
            Debug.Log($" Rigidbody 감지됨{_rb.gameObject.name} 오브젝트에서 찾음");
        }
    }
   

    public void Accelerate(float input)
    {
 
        if (_rb == null)
        {
            Debug.LogError(" Rigidbody가 Car 오브젝트에 없습니다!");
            return;
        }
        

        if (_rb.velocity.magnitude > _maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * _maxSpeed;
            Debug.Log(" 최대 속도 도달: " + _rb.velocity.magnitude);
            return;
        }

        if (input !=0)
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, _maxTorque * input, Time.fixedDeltaTime * _accelerationSpeed);
        }
        else
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.fixedDeltaTime * _decelerationSpeed);
        }
        float appliedForce = Mathf.Lerp(0, _maxTorque * input, Time.fixedDeltaTime * _accelerationSpeed);
        _rb.AddForce(transform.forward * _currentSpeed, ForceMode.Acceleration);
        Debug.Log($" 가속력 적용: {_currentSpeed} | 현재 속도: {_rb.velocity.magnitude}");
    }
    
    public void Brake(bool isBrasking)
    {
        if(isBrasking)
        {
            _rb.velocity *= 0.9f;
        }
    }
}
