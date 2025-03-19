using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

/// <summary>
/// 총알만은 풀링하는 클래스
/// 다양한 총알을 하나의 풀에서 풀링하기 위해 따로 클래스를 만들었다.
/// </summary>
public class BulletPool : MonoBehaviour
{
    // 프리팹별로 큐를 저장하는 딕셔너리
    private Dictionary<string, Queue<GameObject>> _pools = new Dictionary<string, Queue<GameObject>>();

    [SerializeField] private int _poolSize = 20;

    /// <summary>
    /// poolSize만큼 미리 오브젝트를 생성해 비활성화 후 Queue에 보관
    /// </summary>
    public void InitializePool(GameObject prefab)
    {
        string key = prefab.name + "(Clone)";
        if (!_pools.ContainsKey(key))
        {
            _pools[key] = new Queue<GameObject>(); // 새 큐 생성
        }

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(prefab); // 오브젝트 생성
            obj.SetActive(false); // 비활성화
            obj.transform.SetParent(transform);

            _pools[key].Enqueue(obj); // 큐에 추가
            
            obj.GetComponent<Projectile>().SetPool(this); // 풀을 설정
        }
    }

    /// <summary>
    /// 풀에서 오브젝트 하나를 꺼내 활성화 후 반환한다.
    /// </summary>
    public GameObject GetBullet(GameObject bullet)
    {
        string key = bullet.name + "(Clone)";
        if (_pools.ContainsKey(key) && _pools[key].Count > 0)
        {
            GameObject obj = _pools[key].Dequeue(); // 큐에서 꺼냄
            obj.SetActive(true); // 활성화
            return obj;
        }
        else
        {
            // 풀이 비어있으면 새로 생성   
            GameObject newObj = Instantiate(bullet);
            newObj.SetActive(true);
            newObj.transform.SetParent(transform);

            _pools[key].Enqueue(newObj); // 큐에 추가
            newObj.GetComponent<Projectile>().SetPool(this); // 풀을 설정

            return newObj;
        }
    }

    /// <summary>
    /// 사용을 마친 오브젝트를 다시 풀로 돌려놓는다.
    /// </summary>
    public void ReturnBullet(GameObject bullet)
    {
        string key = bullet.name;
        bullet.SetActive(false); // 비활성화

        if (_pools.ContainsKey(key))
        {
            Debug.Log("ReturnBullet");
            _pools[key].Enqueue(bullet); // 큐에 다시 넣음
        }
    }
}
