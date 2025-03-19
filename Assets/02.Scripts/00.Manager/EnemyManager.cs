using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [FoldoutGroup("�糢 ����"), LabelText("�Ϲ� �糢")]
    [SerializeField] private GameObject _commonLukkiPrefab;
    [FoldoutGroup("�糢 ����"), LabelText("���� �糢")]
    [SerializeField] private GameObject _engineerLukkiPrefab;
    [FoldoutGroup("�糢 ����"), LabelText("���� �糢")]
    [SerializeField] private GameObject _mustleLukkiPrefab;
    [FoldoutGroup("�糢 ����"), LabelText("�Ŵ� �糢")]
    [SerializeField] private GameObject _giantLukkiPrefab;

    [BoxGroup("�糢 ����"), LabelText("�ִ� �糢 ��")]
    [SerializeField] private int _maxLukkiCount;
    [BoxGroup("�糢 ����"), LabelText("���� �糢 ��")]
    [SerializeField] private int _currentLukkiCount = 10000;

    void Start()
    {
        Lukki.OnLukkiDeath += LukkiDie;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseLukki()
    {
        // �糢 �� ����
        // float�� int�� ��ȯ�� �� �Ҽ��� ������.
        _currentLukkiCount = Mathf.RoundToInt(1.5f * _currentLukkiCount);

        if(_currentLukkiCount > _maxLukkiCount)
        {
            GameManager.Instance.GameOver();
        }
    }

    private void LukkiDie()
    {
        _currentLukkiCount--;

        if(_currentLukkiCount <= 0)
        {
            GameManager.Instance.Win();
        }
    }
}
