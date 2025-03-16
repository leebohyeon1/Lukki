using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class VehicleController : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Wheel")]
    [SerializeField] private WheelCollider _frontLeftWheel;
    [SerializeField] private WheelCollider _frontRightWheel;
    [SerializeField] private WheelCollider _rearLeftWheel;
    [SerializeField] private WheelCollider _rearRighrWheel;

    [SerializeField] private float _maxSteeringAngle = 40f;
    [SerializeField] private float _brakeTorque = 2000f;

    private Vector2 _moveInput;
    private bool _isBraking = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
