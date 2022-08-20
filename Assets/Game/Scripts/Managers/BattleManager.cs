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
        enemyList[level].SetActive(false);
        level++;
        enemyList[level].SetActive(true);
    }

    private void StartBattle()
    {
        FindObjectsOfType<UnitAI>().Select(unit => unit.isActive = true);
    }
}
