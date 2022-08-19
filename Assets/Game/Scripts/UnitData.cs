using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName= "Unit")]
public class UnitData : ScriptableObject
{
    [Header("Stats")]
    public int attackDamage;
    public float attackSpeed;
    public float attackRange;
    public int health;
    public int level;
    public TeamCategory unitTeam;
    public ClassCategory unitClass;
    public enum TeamCategory { Enemy, Soldier }
    public enum ClassCategory{ Melee, Ranged }
} 
