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
    
    private GameState _currentGameState;

    private void Awake()
    {
        Instance ??= this;
    }

    private void Update()
    {
        switch (_currentGameState)
        {
            case GameState.Battle:
                UIManager.Instance.ResetUI(); 
                FindObjectsOfType<Mergable>().Select(merge => merge.isInBattle = true);
                break;
            case GameState.Win:
                UIManager.Instance.SetUIType(UIType.Win);
                BattleManager.Instance.NextBattle();
                break;
            case GameState.Lose:
                UIManager.Instance.SetUIType(UIType.Lose);
                break;
            case GameState.Shop:
                UIManager.Instance.SetUIType(UIType.Shop);
                break;
            default:
                break;
        }
    }

    public void SetGameState(GameState state)
    {
        _currentGameState = state;
    }
}
