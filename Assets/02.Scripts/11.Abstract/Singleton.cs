using UnityEngine;

/// <summary>
/// 싱글톤 템플릿 클래스
/// 클래스에 이 코드를 상속시키면 싱글톤 인스턴스가 된다.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            // 이미 존재하면 바로 반환
            if (instance != null) return instance;

            // 존재하지 않으면 먼저 찾아보고, 없으면 새로 만든다
            instance = FindObjectOfType<T>();
            if (instance == null)
            {
                var singletonObject = new GameObject(typeof(T).Name);
                instance = singletonObject.AddComponent<T>();
            }

            return instance;
        }
    }

    // 인스턴스가 존재하는지 확인
    public static bool HasInstance => instance != null;

    protected virtual void Awake()
    {
        // 싱글톤 중복 방지
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
