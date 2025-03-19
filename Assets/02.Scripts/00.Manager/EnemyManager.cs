using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [FoldoutGroup("·ç³¢ Á¾·ù"), LabelText("ÀÏ¹Ý ·ç³¢")]
    [SerializeField] private GameObject _commonLukkiPrefab;
    [FoldoutGroup("·ç³¢ Á¾·ù"), LabelText("°øº´ ·ç³¢")]
    [SerializeField] private GameObject _engineerLukkiPrefab;
    [FoldoutGroup("·ç³¢ Á¾·ù"), LabelText("±ÙÀ° ·ç³¢")]
    [SerializeField] private GameObject _mustleLukkiPrefab;
    [FoldoutGroup("·ç³¢ Á¾·ù"), LabelText("°Å´ë ·ç³¢")]
    [SerializeField] private GameObject _giantLukkiPrefab;

    [BoxGroup("·ç³¢ °ü¸®"), LabelText("ÃÖ´ë ·ç³¢ ¼ö")]
    [SerializeField] private int _maxLukkiCount;
    [BoxGroup("·ç³¢ °ü¸®"), LabelText("ÇöÀç ·ç³¢ ¼ö")]
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
        // ·ç³¢ ¼ö Áõ°¡
        // float¸¦ int·Î º¯È¯ÇÒ ¶§ ¼Ò¼öÁ¡ ¹ö¸°´Ù.
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
