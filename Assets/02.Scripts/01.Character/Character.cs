using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��� �����̴� ����ü�� �θ� ������Ʈ ( �÷��̾�, �� ��� )
/// </summary>
public class Character : MonoBehaviour
{
    protected int _maxHp;           // �ִ� ü��
    private int _curHp;             // ���� ü��

    protected float _moveSpeed;     // �����̴� �ӵ�

    protected int _attackDamage;    // ĳ������ �⺻ ���� ���ݷ�


    protected void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Ŭ������ �ʱ�ȭ�ϴ� �Լ��� 
    /// ó�� ���� �ÿ��� ȣ��ȴ�.
    /// </summary>
    protected void Initialize()
    {
        _curHp = _maxHp;
    }

    /// <summary>
    /// ������ �޾��� �� ȣ��Ǵ� �Լ��̰� 
    /// ĳ������ ü���� �����Ѵ�.
    /// </summary>
    /// <param name="damage">�޴� ������</param>
    public void TakeDamage(int damage)
    {
        _curHp -= damage;

        if (_curHp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// �÷��̾��� ü���� 0���� �������� ȣ��Ǹ�
    /// </summary>
    protected void Die()
    {
        Debug.Log($"{gameObject.name}- ����");
    }
}
