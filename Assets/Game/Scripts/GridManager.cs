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
            if (t.isEmpty)
            {
                t.AddMeleeUnit();
                break;
            }
        }
    }
    public void LocateRangedUnit()
    {
        foreach (var t in playerGrids)
        {
            if (t.isEmpty)
            {
                t.AddRangedUnit();
                break;
            }
        }
    }
    
}
