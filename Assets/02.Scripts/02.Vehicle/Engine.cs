using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Engine : MonoBehaviour
{
    [FoldoutGroup("��������")]
    [SerializeField] private float _maxTorque = 2500f;// ���� ��ũ (���� ���̶� �����ϸ��
    [FoldoutGroup("��������")]
    [SerializeField] private float _brakeForce = 2000f; // �극��ũ ��
    [FoldoutGroup("��������")]
    [SerializeField] private float _accelerationSpeed = 5f;
    [FoldoutGroup("��������")]
    [SerializeField] private float _decelerationSpeed = 3f;
    [FoldoutGroup("��������")]
    [SerializeField] private float _maxSpeed = 40f;

    private Rigidbody _rb;
    private float _currentSpeed = 0f;
    
    void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("Rigidbody�� ã�� �� �����ϴ� Car ������Ʈ�� Ȯ���ϼ���.");
        }
        else
        {
            Debug.Log($" Rigidbody ������{_rb.gameObject.name} ������Ʈ���� ã��");
        }
    }
   

    public void Accelerate(float input)
    {
 
        if (_rb == null)
        {
            Debug.LogError(" Rigidbody�� Car ������Ʈ�� �����ϴ�!");
            return;
        }
        

        if (_rb.velocity.magnitude > _maxSpeed)
        {
            _rb.velocity = _rb.velocity.normalized * _maxSpeed;
            Debug.Log(" �ִ� �ӵ� ����: " + _rb.velocity.magnitude);
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
        Debug.Log($" ���ӷ� ����: {_currentSpeed} | ���� �ӵ�: {_rb.velocity.magnitude}");
    }
    
    public void Brake(bool isBrasking)
    {
        if(isBrasking)
        {
            _rb.velocity *= 0.9f;
        }
    }
}
