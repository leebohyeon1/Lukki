using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class CHMouseLook : MonoBehaviour
{
    [FoldoutGroup("Camera Settings"), SerializeField]
    private CinemachineFreeLook _freeLookCamera;
    [FoldoutGroup("Camera Settings"), SerializeField, Range(1f, 10f)]
    private float _rotationSpeed = 3f;
    [FoldoutGroup("Camera Settings"), SerializeField]
    private bool _invertY = false;

    private Vector2 _lookInput;
    private PlayerControll _controls;
    private float _mouseX;
    private float _mouseY;

    private void Awake()
    {
        _controls = new PlayerControll();
        _controls.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        _controls.Player.Look.canceled += ctx => _lookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        _controls.Enable();
        _freeLookCamera.m_XAxis.m_InputAxisName = ""; //  자동 회전 방지
        _freeLookCamera.m_XAxis.m_InputAxisValue = 0; // X축 초기화
        _freeLookCamera.m_YAxis.m_InputAxisName = ""; //  자동 회전 방지
    }

    private void OnDisable() => _controls.Disable();

    private void Update()
    {
        if (_freeLookCamera == null) return;

        if (_lookInput == Vector2.zero) return;


        _mouseX = Mathf.Lerp(_mouseX, _lookInput.x * _rotationSpeed * Time.deltaTime, 0.1f);
        _mouseY = Mathf.Lerp(_mouseY, (_invertY ? 1 : -1) * _lookInput.y * _rotationSpeed * Time.deltaTime, 0.1f);
        _freeLookCamera.m_XAxis.Value += _mouseX;
        _freeLookCamera.m_YAxis.Value = Mathf.Clamp(_freeLookCamera.m_YAxis.Value + _mouseY, 0.1f, 9f);
    }
}
