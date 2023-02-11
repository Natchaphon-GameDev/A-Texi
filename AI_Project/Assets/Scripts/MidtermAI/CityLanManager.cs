using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CityLanManager : MonoBehaviour
{
   public enum Lane
   {
      Lan1,
      Lan2,
      Lan3,
      Lan4,
      Lan5,
      Lan6,
      Lan7
   }
   
   [Serializable]
   public struct LaneProfile
   {
      public Lane lane;
      public List<GameObject> wayPoints;
      public List<GameObject> carPrefabs;
   }
   
   public List<LaneProfile> lanes;
   
   private void Start()
   {
      SpawnCityCar(Lane.Lan1);
      SpawnCityCar(Lane.Lan2);
      SpawnCityCar(Lane.Lan3);
      SpawnCityCar(Lane.Lan4);
      SpawnCityCar(Lane.Lan5);
      SpawnCityCar(Lane.Lan6);
      SpawnCityCar(Lane.Lan7);
   }

   private void SpawnCityCar(Lane laneSelected)
   {
      foreach (var lane in lanes)
      {
         foreach (var car in lane.carPrefabs)
         {
            var laneSpawner = lanes.Find(x => x.lane == laneSelected);
         
            var rndSpawner = Random.Range(0, laneSpawner.wayPoints.Count);

            var spawnAtWp = laneSpawner.wayPoints[rndSpawner];
         
            var carTemp = Instantiate(car, spawnAtWp.transform.position,transform.rotation);
         
            var cityCarController = carTemp.GetComponent<CityCarController>();
            cityCarController.waypoints = laneSpawner.wayPoints;
            cityCarController.currentWP = rndSpawner;
         }
      }
   }
}
