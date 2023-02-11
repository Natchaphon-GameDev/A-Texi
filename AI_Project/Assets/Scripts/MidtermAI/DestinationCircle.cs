using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class DestinationCircle : MonoBehaviour
{
    [SerializeField] private Vector3 rotateSpeed;
    public Destination destination;

    [SerializeField] private GameObject targetSetup;
    private TargetDestinationSetup target;

    private void Start()
    {
        target = targetSetup.GetComponent<TargetDestinationSetup>();
    }

    private void Update()
    {
        transform.Rotate(rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PassengerController>().passenger == null)
        {
            return;
        }
        if (other.CompareTag("Player") && other.GetComponent<Rigidbody>().velocity.sqrMagnitude < 2f)
        {
            var passengerTemp = other.GetComponent<PassengerController>().passenger;
            var destinationTemp = passengerTemp.transform.GetChild(1).GetComponent<EnterTaxiController>();

            if (destinationTemp.destinationTarget == destination)
            {
                Destroy(passengerTemp.transform.GetChild(1).GetComponent<BoxCollider>());
                HandleTargetDestination(destinationTemp);
                GameVariable.isPeopleArrived = true;
                GameVariable.arrowNavi.SetActive(false);
                passengerTemp.gameObject.transform.position = other.transform.position + other.transform.right;
                destinationTemp.renderer.enabled = false;
                passengerTemp.gameObject.SetActive(true);
                StartCoroutine(WaitASec(destinationTemp));
                other.GetComponent<PassengerController>().passenger = null;
            }
        }
    }

    private IEnumerator WaitASec(EnterTaxiController enterTaxiController)
    {
        GameVariable.isPlayerCanMove = false;
        enterTaxiController.HandleCamera();
        yield return new WaitForSeconds(1f);
        GameVariable.isPlayerCanMove = true;
        GameVariable.isPeopleArrived = false;   
        GameVariable.isHasPeople = false;
        enterTaxiController.HandleCamera();
    }

    private void HandleTargetDestination(EnterTaxiController destination)
    {
        switch (destination.destinationTarget)
        {
            case Destination.BagelLand:
                destination.target = target.targetPos[0].pos;
                break;
            case Destination.CityBank:
                destination.target = target.targetPos[1].pos;
                break;
            case Destination.NailSalon:
                destination.target = target.targetPos[2].pos;
                break;
            case Destination.RedApartment:
                destination.target = target.targetPos[3].pos;
                break;
            case Destination.Office:
                destination.target = target.targetPos[4].pos;
                break;
            case Destination.Bank:
                destination.target = target.targetPos[5].pos;
                break;
            case Destination.GreenApartment:
                destination.target = target.targetPos[6].pos;
                break;
            case Destination.GasStation:
                destination.target = target.targetPos[7].pos;
                break;
            case Destination.TheMall:
                destination.target = target.targetPos[8].pos;
                break;
        }
    }
}
