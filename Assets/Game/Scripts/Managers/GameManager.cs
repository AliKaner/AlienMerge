using System;
using System.Linq;
using Game.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public enum GameState
{
    Battle,
    Lose,
    Win,
    Shop
}
public class GameManager: MonoBehaviour
{
    public static GameManager Instance;
    
    public GameState currentGameState = GameState.Shop;

    public  GameObject controller;
    public void Awake()
    {
        if (Instance == null )
        {
            Instance =this;
        }
        else if (Instance != this)
        {
            Debug.LogError($"Another instance of {GetType()} already exist! Destroying self...");
            Destroy(this);
        }
    }

    private void Update()
    {
        switch (currentGameState)
        {
            case GameState.Battle:
                controller.SetActive(false);
                UIManager.Instance.ResetUI();
                CameraManager.Instance.SetBattleCamera();
                break;
            case GameState.Win:
                UIManager.Instance.SetUIType(UIType.Win);
                BattleManager.Instance.NextBattle();
                break;
            case GameState.Lose:
                UIManager.Instance.SetUIType(UIType.Lose);
                break;
            case GameState.Shop:
                controller.SetActive(true);
                UIManager.Instance.SetUIType(UIType.Shop);
                CameraManager.Instance.SetShopCamera();
                break;
        }
    }

    public void SetGameState(GameState state)
    {
        currentGameState = state;
    }
}
