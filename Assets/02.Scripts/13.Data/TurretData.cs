using UnityEngine;

/// <summary>
/// �ͷ��� ������ �����ϴ� ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "NewTurretData", menuName = "Data/TurretData")]
public class TurretData : ScriptableObject
{
    public float FireRate;          // ���� �ӵ�
    public float Damage;            // ���ݷ�
    public float Range;             // ��Ÿ�
    public float ProjectileSpeed;   // ����ü �ӵ� (����ü�� �� ���� ����� ���� ����)

    // �ʿ��� �ɷ�ġ�� �ɼ��� �����Ӱ� �߰�
    // ��: public int upgradeCost; ��
}
