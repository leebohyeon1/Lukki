using UnityEngine;

/// <summary>
/// �̱��� ���ø� Ŭ����
/// Ŭ������ �� �ڵ带 ��ӽ�Ű�� �̱��� �ν��Ͻ��� �ȴ�.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            // �̹� �����ϸ� �ٷ� ��ȯ
            if (instance != null) return instance;

            // �������� ������ ���� ã�ƺ���, ������ ���� �����
            instance = FindObjectOfType<T>();
            if (instance == null)
            {
                var singletonObject = new GameObject(typeof(T).Name);
                instance = singletonObject.AddComponent<T>();
            }

            return instance;
        }
    }

    // �ν��Ͻ��� �����ϴ��� Ȯ��
    public static bool HasInstance => instance != null;

    protected virtual void Awake()
    {
        // �̱��� �ߺ� ����
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
