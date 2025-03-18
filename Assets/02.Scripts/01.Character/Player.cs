using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [FoldoutGroup("����")]
    [SerializeField] private InputManager _inputManager;
    [FoldoutGroup("����")]
    [SerializeField] private VehicleController _vehicleController;

    [FoldoutGroup("�÷��̾� ����"), ReadOnly]
    [SerializeField] private Vector2 _moveInput;
    [FoldoutGroup("�÷��̾� ����"), ReadOnly]
    [SerializeField] private bool _isBraking;


    // Update is called once per frame
    void Update()
    {
        if (_vehicleController == null || _inputManager == null) return;

        _moveInput = _inputManager.MoveInput;
        _isBraking = _inputManager.IsBraking;
        _vehicleController.SetInput(_inputManager.MoveInput, _inputManager.IsBraking);
        
    }
}
