using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
   public bool isGridEmpty;
   private Transform _unitLocation;

   [SerializeField] private GameObject meleeUnit;
   [SerializeField] private GameObject rangedUnit;


   public void MeleeUnitSpawn()
   {
      Instantiate(meleeUnit, _unitLocation.position, _unitLocation.rotation);
   }
   
   public void RangedUnitSpawn()
   {
      Instantiate(rangedUnit, _unitLocation.position, _unitLocation.rotation);
   }
   
   
}
