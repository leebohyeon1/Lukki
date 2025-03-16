using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ������ �������� ���¸� �����ϴ� Ŭ����
/// ������ ó������ �������� ���¸� �����Ѵ�.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public static event Action OnPlayerDeath;   // �ٸ� Ŭ�������� �÷��̾ �׾��� �� ȣ���� �̺�Ʈ

    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

    /// <summary>
    /// ���� �Ŵ��� �ʱ�ȭ �Լ�
    /// ó�� ���� ���� �ÿ��� ȣ��ȴ�.
    /// </summary>
    private void Initialize()
    {

        Player.OnDeath += HandlePlyerDeath;
    }

    private void StartRound()
    {

    }

    /// <summary>
    /// �÷��̾� ���� �� ȣ��
    /// </summary>
    private void HandlePlyerDeath()
    {
        NextDay();

        Player.OnDeath -= HandlePlyerDeath;
    }

    /// <summary>
    /// ���� ���� �Ѿ�� �Լ�
    /// </summary>
    private void NextDay()
    {

    }

}
