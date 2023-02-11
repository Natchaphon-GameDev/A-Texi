using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariable : MonoBehaviour
{
   public static bool isHasPeople;
   public static bool isPickingPeople;
   public static bool isPeopleArrived;
   public static bool isPlayerCanMove;
   public static GameObject arrowNavi;

   private void Start()
   {
      arrowNavi = Camera.main.transform.GetChild(0).gameObject;
   }
}
