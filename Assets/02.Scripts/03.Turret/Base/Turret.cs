using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 터렛의 부모 클래스
/// </summary>
public class Turret : MonoBehaviour
{
    [SerializeField] private TurretData _turretData; // 스크립터블 오브젝트 참조
  
    protected virtual void Awake()
    {
        Initialize();
    }
    
    protected virtual void Initialize()
    {
    }

}
