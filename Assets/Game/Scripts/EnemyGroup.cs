using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [SerializeField] private GameObject[] units;
    public int deathCount = 0;

    private void Update()
    {
        if (deathCount >= units.Length)
        {
            GameManager.Instance.SetGameState(GameState.Win);
        }
    }
}
