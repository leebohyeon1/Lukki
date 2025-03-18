using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class Player : Character
{
    public static event Action OnDeath;

    [FoldoutGroup("참조")]
    [SerializeField] private InputManager _inputManager;
    [FoldoutGroup("참조")]
    [SerializeField] private VehicleController _vehicleController;

    [FoldoutGroup("플레이어 상태"), ReadOnly]
    [SerializeField] private Vector2 _moveInput;
    [FoldoutGroup("플레이어 상태"), ReadOnly]
    [SerializeField] private bool _isBraking;


    // Update is called once per frame
    void Update()
    {
        if (_vehicleController == null || _inputManager == null) return;

        _moveInput = _inputManager.MoveInput;
        _isBraking = _inputManager.IsBraking;
        _vehicleController.SetInput(_inputManager.MoveInput, _inputManager.IsBraking);

    }
    [Button]
    protected override void Die()
    {
        base.Die();

        // 플레이어 죽었을 때 이벤트 발생
        OnDeath?.Invoke();
      
    }
}
