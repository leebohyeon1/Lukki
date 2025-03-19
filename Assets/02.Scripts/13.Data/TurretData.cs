using UnityEngine;

/// <summary>
/// 터렛의 정보를 저장하는 ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "NewTurretData", menuName = "Data/TurretData")]
public class TurretData : ScriptableObject
{
    public float FireRate;          // 연사 속도
    public float Damage;            // 공격력
    public float Range;             // 사거리
    public float ProjectileSpeed;   // 투사체 속도 (투사체가 이 값을 사용할 수도 있음)

    // 필요한 능력치나 옵션을 자유롭게 추가
    // 예: public int upgradeCost; 등
}
