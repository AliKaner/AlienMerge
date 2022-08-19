using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Grid[] playerGrids;
    
    public void LocateMeleeUnit()
    {
        foreach (var t in playerGrids)
        {
            if (t.isGridEmpty == true)
            {
                t.MeleeUnitSpawn();
                break;
            }
        }
    }
    public void LocateRangedUnit()
    {
        for (var i = 0; i < playerGrids.Length; i++)
        {
            if (playerGrids[i].isGridEmpty == true)
            {
                playerGrids[i].RangedUnitSpawn();
                break;
            }
        }
    }
    
}
