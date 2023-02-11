using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnterTaxiController : MonoBehaviour
{
   [Header("Setup")]
   [SerializeField] private GameObject character;
   [SerializeField] private float moveSpeed;
   [SerializeField] private float rotateSpeed;
   
   public Destination destinationTarget;

   private GameObject player;
   [Header("Do not Set")]
   public new MeshRenderer renderer;
   public Transform target;

   private void Start()
   {
      renderer = GetComponent<MeshRenderer>();
   }

   private IEnumerator WaitASec(Collider other)
   {
      yield return new WaitForSeconds(1f);
      GameVariable.isPlayerCanMove = false;
      GameVariable.isPickingPeople = true;
      player = other.gameObject;
      HandleCamera();
   }

   private void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("Player") && other.GetComponent<Rigidbody>().velocity.sqrMagnitude < 2f && !GameVariable.isPeopleArrived && !GameVariable.isHasPeople)
      {
         StartCoroutine(WaitASec(other));
      }
   }
   
   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag("Player") && !GameVariable.isPickingPeople && !GameVariable.isPeopleArrived)
      {
         StopAllCoroutines();
      }
   }
   
   public void HandleCamera()
   {
      var cam = Camera.main.GetComponent<CameraFollow>();

      if (GameVariable.isPlayerCanMove)
      {
         cam.target = null;
         cam.enabled = false;
         return;
      }
      
      cam.target = character.transform;
      cam.enabled = true;
   }

   private void HandleMove()
   {
      if (GameVariable.isPeopleArrived && !renderer.enabled)
      {
         character.transform.Translate((Vector3.forward * moveSpeed) * Time.deltaTime);
         return;
      }
      
      if (player != null)
      {
         character.transform.Translate((Vector3.forward * moveSpeed) * Time.deltaTime);
      }
   }

   private void HandleRotate()
   {
      if (!renderer.enabled)
      {
         var distance = Vector3.Distance(target.position, character.transform.position);

         if (distance < .1f)
         {
            HandleCamera();
            Destroy(transform.parent.gameObject);
         }

         var lookAtTarget = Quaternion.LookRotation(target.transform.position - character.transform.position);
         character.transform.rotation = Quaternion.Slerp(character.transform.rotation, lookAtTarget, rotateSpeed * Time.deltaTime);
         return;
      }
      
      if (player != null)
      {
         var lookAtTarget = Quaternion.LookRotation(player.transform.position - character.transform.position);
         character.transform.rotation = Quaternion.Slerp(character.transform.rotation, lookAtTarget, rotateSpeed * Time.deltaTime);
      }
   }

   private void Update()
   {
     HandleMove();
     HandleRotate();
   }
}
