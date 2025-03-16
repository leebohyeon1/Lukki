using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 오브젝트를 풀링하여 관리하는 클래스
/// 풀링할 오브젝트를 미리 생성해두고 필요할 때마다 꺼내 사용하고 반환한다.
/// 원할 경우 여러 오브젝트를 한번에 풀링하도록 만들어도 된다.
/// </summary>
public class ObjectPool : MonoBehaviour
{
    private GameObject _prefab;
    [SerializeField] private int _poolSize = 20;

    // 오브젝트를 보관할 큐(Queue)
    private Queue<GameObject> _objectPool = new Queue<GameObject>();

    /// <summary>
    /// poolSize만큼 미리 오브젝트를 생성해 비활성화 후 Queue에 보관
    /// </summary>
    public void InitializePool(GameObject prefab)
    {
        if(_prefab == null)
        {
            _prefab = prefab;
        }

        // 이미 생성되어 있다면 중복 생성 방지 (상황에 따라 추가)
        if (_objectPool.Count > 0)
            return;

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject poolingObj = Instantiate(_prefab, transform);
            poolingObj.SetActive(false);

            // PoolingObject 컴포넌트를 찾은 뒤, 이 오브젝트가 돌아갈 풀을 지정해준다
            PoolingObject obj = poolingObj.GetComponent<PoolingObject>();
            if (obj != null)
            {
                obj.SetPool(this);
            }

            _objectPool.Enqueue(poolingObj);
        }
    }

    /// <summary>
    /// 풀에서 오브젝트 하나를 꺼내 활성화 후 반환한다.
    /// </summary>
    public GameObject GetBullet()
    {
        // 큐가 비어있지 않다면 꺼내서 사용
        if (_objectPool.Count > 0)
        {
            GameObject bullet = _objectPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // 풀이 모자랄 경우 새로 생성 (상황에 맞게 처리)
            GameObject newObj = Instantiate(_prefab, transform);
            // 새로 만든 것도 풀 정보를 넘겨줘야 한다
            PoolingObject obj = newObj.GetComponent<PoolingObject>();
            if (obj != null)
            {
                obj.SetPool(this);
            }
            return newObj;
        }
    }

    /// <summary>
    /// 사용을 마친 오브젝트를 다시 풀로 돌려놓는다.
    /// </summary>
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform); // 풀 오브젝트 밑으로 다시 귀속
        _objectPool.Enqueue(obj);
    }
}
