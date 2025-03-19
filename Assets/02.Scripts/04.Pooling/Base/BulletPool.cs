using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ѿ˸��� Ǯ���ϴ� Ŭ����
/// �پ��� �Ѿ��� �ϳ��� Ǯ���� Ǯ���ϱ� ���� ���� Ŭ������ �������.
/// </summary>
public class BulletPool : MonoBehaviour
{
    private List<Queue<GameObject>> _poolList = new List<Queue<GameObject>>();
    private List<GameObject> _bulletList = new List<GameObject>();
    [SerializeField] private int _poolSize = 20;

    /// <summary>
    /// poolSize��ŭ �̸� ������Ʈ�� ������ ��Ȱ��ȭ �� Queue�� ����
    /// </summary>
    public void InitializePool(int turretIndex, GameObject prefab)
    {

        Queue<GameObject> pool = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(prefab); // ������Ʈ ����
            obj.SetActive(false); // ��Ȱ��ȭ
            obj.transform.SetParent(transform);
            
            pool.Enqueue(obj);

            obj.GetComponent<Projectile>().SetPool(this, turretIndex); // Ǯ�� ����
        }

        _bulletList.Add(prefab);
        _poolList.Add(pool);

    }

    /// <summary>
    /// Ǯ���� ������Ʈ �ϳ��� ���� Ȱ��ȭ �� ��ȯ�Ѵ�.
    /// </summary>
    public GameObject GetBullet(int turretIndex)
    {
        if (_poolList[turretIndex].Count > 0)
        {
            GameObject obj = _poolList[turretIndex].Dequeue(); // ť���� ����
            obj.SetActive(true); // Ȱ��ȭ
            return obj;
        }
        else
        {
            // Ǯ�� ��������� ���� ����   
            GameObject newObj = Instantiate(_bulletList[turretIndex]);
            newObj.SetActive(true);
            newObj.transform.SetParent(transform);

            _poolList[turretIndex].Enqueue(newObj); // ť�� �߰�
            newObj.GetComponent<Projectile>().SetPool(this, turretIndex); // Ǯ�� ����

            return newObj;
        }
    }

    /// <summary>
    /// ����� ��ģ ������Ʈ�� �ٽ� Ǯ�� �������´�.
    /// </summary>
    public void ReturnBullet(int turretIndex, GameObject bullet)
    {
        bullet.SetActive(false); // ��Ȱ��ȭ

        _poolList[turretIndex].Enqueue(bullet); // ť�� �ٽ� ����
    }
}
