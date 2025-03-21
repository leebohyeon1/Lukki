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
    private Rigidbody _rb;

    private float _smoothSteering = 0f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if(!_engine || !_wheel)
        {
            Debug.LogError("Engine �Ǵ� Wheel ������Ʈ �� ����");
        }

        _rb.centerOfMass = new Vector3(0, -1.5f, 0); // ������ �������� ���ؼ� �����߽��� ���� �ʿ��  Y�� ����
        Cursor.lockState = CursorLockMode.Locked;
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
        ApplyGroundAlignment();
        ApplyGravityControl();
    }

    private void ApplyGroundAlignment()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 2f))
        {
            Vector3 groundNormal = hit.normal;
            Quaternion groundRotation = Quaternion.FromToRotation(transform.up, groundNormal);
            transform.rotation = Quaternion.Lerp(transform.rotation, groundRotation * transform.rotation, Time.fixedDeltaTime * 5f);
        }
    }

    private void ApplyGravityControl()
    {
        if (!IsGrounded())
        {
            _rb.AddForce(Vector3.down * 60f, ForceMode.Acceleration);
            _rb.AddTorque(transform.right * 5f, ForceMode.Acceleration);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 2.5f);
    }

}
