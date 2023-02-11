using System;
using System.Collections.Generic;
using UnityEngine;

namespace CameraController
{
    public class CameraController : MonoBehaviour
    {
        [Range(1,10)]
        [SerializeField] private float translateSpeed = 10;
        [Range(1,10)]
        [SerializeField] private float rotationSpeed = 5;
        [Range(1,10)]
        [SerializeField] private float planSpeed = 4;

        [SerializeField] private Vector3 camOffset;
        [SerializeField] private GameObject car;

        private int checkCamera; //Check where is Camera mode now

        private void Start()
        {
            GameManager.instance.OnMenu += SetMenuCam;
            GameManager.instance.OnRestart += SetPlayerCam;
        }

        private void LateUpdate()
        {
            HandlePlanCam();
            HandleTranslation();
            HandleRotation();
        }
        
        private void SetMenuCam()
        {
            enabled = false;
        }
	
        private void SetPlayerCam()
        {
            enabled = true;
        }

        private void HandlePlanCam()
        {
            var Xinput = Input.GetAxis("Horizontal");
            var Yinput = Input.GetAxis("Vertical");
            
            if (Yinput == 1 && Xinput == 0)
            {
                if (camOffset.x > 0)
                {
                    if (camOffset.x == 0)
                    {
                        return;
                    }
                    camOffset.x -= planSpeed * Time.deltaTime;
                }
                else
                {
                    if (camOffset.x == 0)
                    {
                        return;
                    }
                    camOffset.x += planSpeed * Time.deltaTime;
                }
            }

            if (Xinput < 0 && camOffset.x > -3)
            {
                camOffset.x -= planSpeed * Time.deltaTime;
            }
            else if (Xinput > 0 && camOffset.x < 3)
            {
                camOffset.x += planSpeed * Time.deltaTime;
            }
        }

        private void HandleRotation()
        {
            //Rotation Camera
            var direction = car.transform.position - transform.position;
            var rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        private void HandleTranslation()
        {
            //Following Camera
            var targetPosition = car.transform.TransformPoint(camOffset);
            transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
        }
    }
}