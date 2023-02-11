using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerController : MonoBehaviour
{
    public GameObject passenger;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("People") && !GameVariable.isHasPeople && GameVariable.isPickingPeople && !GameVariable.isPeopleArrived)
        {
            var peopleTemp = collision.gameObject.transform.parent.gameObject;
            GameVariable.arrowNavi.SetActive(true);
            HandleDestinationTarget(peopleTemp);
            GameVariable.isPlayerCanMove = true;
            GameVariable.isPickingPeople = false;
            GameVariable.isHasPeople = true;
            peopleTemp.transform.GetChild(1).gameObject.GetComponent<EnterTaxiController>().HandleCamera();
            passenger = peopleTemp;
            peopleTemp.gameObject.SetActive(false);
        }
    }

    private void HandleDestinationTarget(GameObject people)
    {
        var taxiControl = people.transform.GetChild(1).GetComponent<EnterTaxiController>();
        var naviControl = GameVariable.arrowNavi.transform.GetChild(0).GetComponent<NaviController>();
        
        switch (taxiControl.destinationTarget)
        {
            case Destination.BagelLand:
                naviControl.MoveTo(naviControl.FindDestination("BagelLand"));
                break;
            case Destination.CityBank:
                naviControl.MoveTo(naviControl.FindDestination("CityBank"));
                break;
            case Destination.NailSalon:
                naviControl.MoveTo(naviControl.FindDestination("NailSalon"));
                break;
            case Destination.RedApartment:
                naviControl.MoveTo(naviControl.FindDestination("RedApartment"));
                break;
            case Destination.Office:
                naviControl.MoveTo(naviControl.FindDestination("Office"));
                break;
            case Destination.Bank:
                naviControl.MoveTo(naviControl.FindDestination("Bank"));
                break;
            case Destination.GreenApartment:
                naviControl.MoveTo(naviControl.FindDestination("GreenApartment"));
                break;
            case Destination.GasStation:
                naviControl.MoveTo(naviControl.FindDestination("GasStation"));
                break;
            case Destination.TheMall:
                naviControl.MoveTo(naviControl.FindDestination("TheMall"));
                break;
        }
    }
}
