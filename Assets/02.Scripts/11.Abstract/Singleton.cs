using UnityEngine;

/// <summary>
/// �̱��� ���ø� Ŭ����
/// Ŭ������ �� �ڵ带 ��ӽ�Ű�� �̱��� �ν��Ͻ��� �ȴ�.
/// </summary>
/// <typeparam name="T">�̱��� Ŭ���� Ÿ��</typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static  T _instance;
    public static T Instance
    {
        get
        {
            // �̹� �����ϸ� �ٷ� ��ȯ
            if (_instance == null)
            {
                // �������� ������ ���� ã�ƺ���, ������ ���� �����
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
    /// �̱��� �ν��Ͻ��� ã�ų� �����ϴ� �Լ�
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
    /// �ߺ��� �̱��� ������Ʈ�� �����ϴ� �Լ�   
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
