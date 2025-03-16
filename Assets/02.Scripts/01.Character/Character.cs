using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 모든 움직이는 물체의 부모 오브젝트 ( 플레이어, 적 등등 )
/// </summary>
public class Character : MonoBehaviour
{
    protected int _maxHp;           // 최대 체력
    private int _curHp;             // 현재 체력

    protected void Start()
    {
        Initialize();
    }

    /// <summary>
    /// 클래스를 초기화하는 함수로 
    /// 처음 시작 시에만 호출된다.
    /// </summary>
    protected virtual void Initialize()
    {
        _curHp = _maxHp;
    }

    /// <summary>
    /// 공격을 받았을 때 호출되는 함수이고 
    /// 캐릭터의 체력을 감소한다.
    /// </summary>
    /// <param name="damage">받는 데미지</param>
    public virtual void TakeDamage(int damage)
    {
        _curHp -= damage;

        if (_curHp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 플레이어의 체력이 0보다 낮아지면 호출되면
    /// </summary>
    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} - 죽음");
    }
}
