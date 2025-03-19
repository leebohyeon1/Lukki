using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

/// <summary>
/// �Ѿ˸��� Ǯ���ϴ� Ŭ����
/// �پ��� �Ѿ��� �ϳ��� Ǯ���� Ǯ���ϱ� ���� ���� Ŭ������ �������.
/// </summary>
public class BulletPool : MonoBehaviour
{
    // �����պ��� ť�� �����ϴ� ��ųʸ�
    private Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

    [SerializeField] private int _poolSize = 20;

    /// <summary>
    /// poolSize��ŭ �̸� ������Ʈ�� ������ ��Ȱ��ȭ �� Queue�� ����
    /// </summary>
    public void InitializePool(GameObject prefab)
    {
        string key = prefab.name + "(Clone)";
        if (!_pools.ContainsKey(key))
        {
            _pools[key] = new Queue<GameObject>(); // �� ť ����
        }

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(prefab); // ������Ʈ ����
            obj.SetActive(false); // ��Ȱ��ȭ
            obj.transform.SetParent(transform);

            _pools[key].Enqueue(obj); // ť�� �߰�
            
            obj.GetComponent<Projectile>().SetPool(this); // Ǯ�� ����
        }
    }

    /// <summary>
    /// Ǯ���� ������Ʈ �ϳ��� ���� Ȱ��ȭ �� ��ȯ�Ѵ�.
    /// </summary>
    public GameObject GetBullet(GameObject bullet)
    {
        string key = bullet.name + "(Clone)";
        if (_pools.ContainsKey(key) && _pools[key].Count > 0)
        {
            GameObject obj = _pools[key].Dequeue(); // ť���� ����
            obj.SetActive(true); // Ȱ��ȭ
            return obj;
        }
        else
        {
            // Ǯ�� ��������� ���� ����   
            GameObject newObj = Instantiate(bullet);
            newObj.SetActive(true);
            newObj.transform.SetParent(transform);

            _pools[key].Enqueue(newObj); // ť�� �߰�
            newObj.GetComponent<Projectile>().SetPool(this); // Ǯ�� ����

            return newObj;
        }
    }

    /// <summary>
    /// ����� ��ģ ������Ʈ�� �ٽ� Ǯ�� �������´�.
    /// </summary>
    public void ReturnBullet(GameObject bullet)
    {
        string key = bullet.name;
        bullet.SetActive(false); // ��Ȱ��ȭ

        if (_pools.ContainsKey(key))
        {
            Debug.Log("ReturnBullet");
            _pools[key].Enqueue(bullet); // ť�� �ٽ� ����
        }
    }
}
