using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// �ͷ��� �θ� Ŭ����
/// </summary>
public class Turret : MonoBehaviour
{
    public TurretData TurretData => _turretData; // ������Ƽ�� ����
    [LabelText("�ͷ� ������")]
    [SerializeField] private TurretData _turretData; // ��ũ���ͺ� ������Ʈ ����
    
    
    protected virtual void Awake()
    {
        Initialize();
    }
    
    protected virtual void Initialize()
    {
    }

}
