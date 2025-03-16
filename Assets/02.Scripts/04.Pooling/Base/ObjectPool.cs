using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʈ�� Ǯ���Ͽ� �����ϴ� Ŭ����
/// Ǯ���� ������Ʈ�� �̸� �����صΰ� �ʿ��� ������ ���� ����ϰ� ��ȯ�Ѵ�.
/// ���� ��� ���� ������Ʈ�� �ѹ��� Ǯ���ϵ��� ���� �ȴ�.
/// </summary>
public class ObjectPool : MonoBehaviour
{
    private GameObject _prefab;
    [SerializeField] private int _poolSize = 20;

    // ������Ʈ�� ������ ť(Queue)
    private Queue<GameObject> _objectPool = new Queue<GameObject>();

    /// <summary>
    /// poolSize��ŭ �̸� ������Ʈ�� ������ ��Ȱ��ȭ �� Queue�� ����
    /// </summary>
    public void InitializePool(GameObject prefab)
    {
        if(_prefab == null)
        {
            _prefab = prefab;
        }

        // �̹� �����Ǿ� �ִٸ� �ߺ� ���� ���� (��Ȳ�� ���� �߰�)
        if (_objectPool.Count > 0)
            return;

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject poolingObj = Instantiate(_prefab, transform);
            poolingObj.SetActive(false);

            // PoolingObject ������Ʈ�� ã�� ��, �� ������Ʈ�� ���ư� Ǯ�� �������ش�
            PoolingObject obj = poolingObj.GetComponent<PoolingObject>();
            if (obj != null)
            {
                obj.SetPool(this);
            }

            _objectPool.Enqueue(poolingObj);
        }
    }

    /// <summary>
    /// Ǯ���� ������Ʈ �ϳ��� ���� Ȱ��ȭ �� ��ȯ�Ѵ�.
    /// </summary>
    public GameObject GetBullet()
    {
        // ť�� ������� �ʴٸ� ������ ���
        if (_objectPool.Count > 0)
        {
            GameObject bullet = _objectPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            // Ǯ�� ���ڶ� ��� ���� ���� (��Ȳ�� �°� ó��)
            GameObject newObj = Instantiate(_prefab, transform);
            // ���� ���� �͵� Ǯ ������ �Ѱ���� �Ѵ�
            PoolingObject obj = newObj.GetComponent<PoolingObject>();
            if (obj != null)
            {
                obj.SetPool(this);
            }
            return newObj;
        }
    }

    /// <summary>
    /// ����� ��ģ ������Ʈ�� �ٽ� Ǯ�� �������´�.
    /// </summary>
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform); // Ǯ ������Ʈ ������ �ٽ� �ͼ�
        _objectPool.Enqueue(obj);
    }
}
