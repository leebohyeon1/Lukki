using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;

/// <summary>
/// New Input System으로 플레이어 컨트롤 및 UI컨트롤
/// </summary>
public class InputManager : MonoBehaviour
{
    private PlayerControll _controls;

    [FoldoutGroup("InputValues"), ReadOnly]
    [SerializeField] private Vector2 _moveInput;
    [FoldoutGroup("InputValues"), ReadOnly]
    [SerializeField] private bool _isBraking;
    [FoldoutGroup("InputValues"), ReadOnly]
    [SerializeField] private Vector2 _lookInput;
    
    public Vector2 MoveInput => _moveInput;
    public Vector2 LookInput => _lookInput;
    public bool IsBraking => _isBraking;

    private void Awake()
    {
        _controls = new PlayerControll();
        _controls.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _controls.Player.Move.canceled += ctx => _moveInput = Vector2.zero;

        _controls.Player.Brake.performed += ctx => _isBraking = true;
        _controls.Player.Brake.canceled += ctx => _isBraking = false;

        _controls.Player.Look.performed += ctx => _lookInput = ctx.ReadValue<Vector2>();
        _controls.Player.Look.canceled += ctx => _lookInput = Vector2.zero;
    }

    private void OnEnable() => _controls.Enable();
    private void OnDisable() => _controls.Disable();
   
}
