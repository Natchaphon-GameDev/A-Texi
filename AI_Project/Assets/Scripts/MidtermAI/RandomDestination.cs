using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomDestination : MonoBehaviour
{
    [SerializeField] private List<GameObject> peoples;

    private void Start()
    {
        
        foreach (var people in peoples)
        {
            //Create Random 0-9
            var temp = Random.Range(0,9);
            
            //Separate Destination too Random
            people.transform.GetChild(1).GetComponent<EnterTaxiController>().destinationTarget = temp switch
            {
                0 => Destination.BagelLand,
                1 => Destination.CityBank,
                2 => Destination.NailSalon,
                3 => Destination.RedApartment,
                4 => Destination.Office,
                5 => Destination.Bank,
                6 => Destination.GreenApartment,
                7 => Destination.GasStation,
                8 => Destination.TheMall,
                _ => people.GetComponent<EnterTaxiController>().destinationTarget
            };
        }
    }
}
