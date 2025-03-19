using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 풀링가능한 오브젝트의 부모 클래스
/// </summary>
public class PoolingObject : MonoBehaviour
{
    private ObjectPool _objectPool;

    /// <summary>
    /// 이 오브젝트가 귀속될 풀을 지정하는 메서드
    /// </summary>
    public void SetPool(ObjectPool pool)
    {
        _objectPool = pool;
    }

    protected void ReturnToPool()
    {
        if (_objectPool == null)
        {
            // 만약 풀 정보가 없다면 그냥 파괴
            Destroy(gameObject);
        }
        else
        {
            _objectPool.ReturnObject(gameObject);
        }
    }

}
