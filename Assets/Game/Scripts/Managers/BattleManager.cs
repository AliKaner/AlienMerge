using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public int level;
    [SerializeField] private GameObject[] enemyList;
    public GridManager gridManager;
    
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

    public void NextBattle()
    { 
        Destroy(enemyList[level]);
        level++;
        enemyList[level].SetActive(true);
        GoldManager.Instance.GainGold(100*level);
        GameManager.Instance.SetGameState(GameState.Shop);
        
    }

    public void StartBattle()
    {
        Debug.Log("DNS");
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<AI>().enabled = true;
        }
        var alies = GameObject.FindGameObjectsWithTag("Ally");
        foreach (var enemy in alies)
        {
            enemy.GetComponent<AI>().enabled = true;
        }
        GameManager.Instance.SetGameState(GameState.Battle);
    }
    
}
