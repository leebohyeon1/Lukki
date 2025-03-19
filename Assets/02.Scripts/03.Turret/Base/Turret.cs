using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 터렛의 부모 클래스
/// </summary>
public class Turret : MonoBehaviour
{
    public TurretData TurretData => _turretData; // 프로퍼티로 변경
    [LabelText("터렛 데이터")]
    [SerializeField] private TurretData _turretData; // 스크립터블 오브젝트 참조

    protected int _installedNumber = -1; // 터렛 장착 순서

    protected virtual void Awake()
    {
        Initialize();
    }
    
    protected virtual void Initialize()
    {
    }

}
