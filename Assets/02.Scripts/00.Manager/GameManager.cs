using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 게임의 전반적인 상태를 관리하는 클래스
/// 게임의 처음부터 끝까지의 상태를 관리한다.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public static event Action OnPlayerDeath;   // 다른 클래스에서 플레이어가 죽었을 때 호출할 이벤트

    private EnemyManager _enemyManager; // 적 관리 클래스

    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

    /// <summary>
    /// 게임 매니저 초기화 함수
    /// 처음 게임 시작 시에만 호출된다.
    /// </summary>
    private void Initialize()
    {
        Player.OnDeath += HandlePlyerDeath;

        OnPlayerDeath += HandlePlyerDeath;
    }

    private void StartRound()
    {

    }

    /// <summary>
    /// 플레이어 죽을 시 호출
    /// </summary>
    private void HandlePlyerDeath()
    {
        NextDay();

        Player.OnDeath -= HandlePlyerDeath;
    }

    /// <summary>
    /// 다음 날로 넘어가는 함수
    /// </summary>
    private void NextDay()
    {
        _enemyManager.IncreaseLukki();
    }

    public void Win()
    {
        Debug.Log("게임 승리"); 
    }

    public void GameOver()
    {
        Debug.Log("게임 오버");
    }

}
