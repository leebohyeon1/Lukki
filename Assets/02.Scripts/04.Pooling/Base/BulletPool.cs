using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 총알만은 풀링하는 클래스
/// 다양한 총알을 하나의 풀에서 풀링하기 위해 따로 클래스를 만들었다.
/// </summary>
public class BulletPool : MonoBehaviour
{
    private List<Queue<GameObject>> _poolList = new List<Queue<GameObject>>();
    private List<GameObject> _bulletList = new List<GameObject>();
    [SerializeField] private int _poolSize = 20;

    /// <summary>
    /// poolSize만큼 미리 오브젝트를 생성해 비활성화 후 Queue에 보관
    /// </summary>
    public void InitializePool(int turretIndex, GameObject prefab)
    {

        Queue<GameObject> pool = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(prefab); // 오브젝트 생성
            obj.SetActive(false); // 비활성화
            obj.transform.SetParent(transform);
            
            pool.Enqueue(obj);

            obj.GetComponent<Projectile>().SetPool(this, turretIndex); // 풀을 설정
        }

        _bulletList.Add(prefab);
        _poolList.Add(pool);

    }

    /// <summary>
    /// 풀에서 오브젝트 하나를 꺼내 활성화 후 반환한다.
    /// </summary>
    public GameObject GetBullet(int turretIndex)
    {
        if (_poolList[turretIndex].Count > 0)
        {
            GameObject obj = _poolList[turretIndex].Dequeue(); // 큐에서 꺼냄
            obj.SetActive(true); // 활성화
            return obj;
        }
        else
        {
            // 풀이 비어있으면 새로 생성   
            GameObject newObj = Instantiate(_bulletList[turretIndex]);
            newObj.SetActive(true);
            newObj.transform.SetParent(transform);

            _poolList[turretIndex].Enqueue(newObj); // 큐에 추가
            newObj.GetComponent<Projectile>().SetPool(this, turretIndex); // 풀을 설정

            return newObj;
        }
    }

    /// <summary>
    /// 사용을 마친 오브젝트를 다시 풀로 돌려놓는다.
    /// </summary>
    public void ReturnBullet(int turretIndex, GameObject bullet)
    {
        bullet.SetActive(false); // 비활성화

        _poolList[turretIndex].Enqueue(bullet); // 큐에 다시 넣음
    }
}
