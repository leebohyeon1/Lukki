using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ͷ��� �θ� Ŭ����
/// </summary>
public class Turret : MonoBehaviour
{
    [SerializeField] private TurretData _turretData; // ��ũ���ͺ� ������Ʈ ����
  
    protected virtual void Awake()
    {
        Initialize();
    }
    
    protected virtual void Initialize()
    {
    }

}
