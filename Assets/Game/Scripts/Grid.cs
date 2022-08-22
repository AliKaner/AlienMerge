using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool isMerging;
    public bool isEmpty;
    public int level;
    public UnitData.TypeCategory type;
    public GameObject[] ranged;
    public GameObject[] melee;
    public GameObject currentUnit;
    public Transform spawnLocation;
    
    public void AddRangedUnit()
    {
        currentUnit = Instantiate(ranged[0],spawnLocation.position,spawnLocation.rotation,transform);
        isEmpty = false;
        level = 1;
        type = UnitData.TypeCategory.Ranged;
    }
    public void AddMeleeUnit()
    {
        currentUnit = Instantiate(melee[0],spawnLocation.position,spawnLocation.rotation,transform);
        isEmpty = false;
        level = 1;
        type = UnitData.TypeCategory.Melee;
    }

    public void SpawnUnit()
    {
        currentUnit = Instantiate(currentUnit,spawnLocation.position,spawnLocation.rotation,gameObject.transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grid") && isMerging)
        {
            Merge(other.GetComponent<Grid>());
        }
    }

    public void Merge(Grid grid)
    {
        if (grid.isEmpty)
        {
            grid.currentUnit = currentUnit;
            grid.SpawnUnit();
        }
        else if (level == grid.level && type == grid.type)
        {
            switch(type)
            {
                case UnitData.TypeCategory.Melee:
                    Destroy(currentUnit);
                    Destroy(grid.currentUnit);
                    grid.currentUnit = grid.melee[level];
                    grid.SpawnUnit();
                    level--;
                    grid.level++;
                    isEmpty = true;
                    break;
                case UnitData.TypeCategory.Ranged:
                    Destroy(currentUnit);
                    grid.currentUnit = grid.ranged[level];
                    Destroy(grid.currentUnit);
                    grid.SpawnUnit();
                    level--;
                    grid.level++;
                    isEmpty = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            
        }
     
    }
    
}
