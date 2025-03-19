using UnityEngine;

/// <summary>
/// 싱글톤 템플릿 클래스
/// 클래스에 이 코드를 상속시키면 싱글톤 인스턴스가 된다.
/// </summary>
/// <typeparam name="T">싱글톤 클래스 타입</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static  T _instance;
    public static T Instance
    {
        get
        {
            // 이미 존재하면 바로 반환
            if (_instance == null)
            {
                // 존재하지 않으면 먼저 찾아보고, 없으면 새로 만든다
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    SetupInstance();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
       
        RemoveDuplicates();
    }

    /// <summary>
    /// 싱글톤 인스턴스를 찾거나 생성하는 함수
    /// </summary>
    private static void SetupInstance()
    {
        _instance = (T)FindObjectOfType(typeof(T));
        if (_instance == null)
        {
            GameObject gameObj = new GameObject(typeof(T).Name);
            _instance = gameObj.AddComponent<T>();
            DontDestroyOnLoad(gameObj);
        }
    }

    /// <summary>
    /// 중복된 싱글톤 오브젝트를 제거하는 함수   
    /// </summary>
    private void RemoveDuplicates()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
