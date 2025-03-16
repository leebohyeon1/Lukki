using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ǯ�������� ������Ʈ�� �θ� Ŭ����
/// </summary>
public class PoolingObject : MonoBehaviour
{
    private ObjectPool _objectPool;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �� ������Ʈ�� �ͼӵ� Ǯ�� �����ϴ� �޼���
    /// </summary>
    public void SetPool(ObjectPool pool)
    {
        _objectPool = pool;
    }

    protected virtual void ReturnToPool()
    {
        if (_objectPool == null)
        {
            // ���� Ǯ ������ ���ٸ� �׳� �ı�
            Destroy(gameObject);
        }
        else
        {
            _objectPool.ReturnObject(gameObject);
        }
    }

}
