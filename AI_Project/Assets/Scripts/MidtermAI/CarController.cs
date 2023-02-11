using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : BaseCar
{
    [SerializeField] private List<Wheels> wheels;
    
    [SerializeField] private float FrontWheelMotorForce;
    [SerializeField] private float RearWheelMotorForce;
    [SerializeField] private float BreakForce;
    
    [SerializeField] private float centerOfMass = -0.3f; //centerOfMass help car not easy to flip
    [SerializeField] private float massOfCar;
    [SerializeField] private float maxSteeringAngle;

    
    private float currentsteerAngle;
    private float currentbreakForce;
    private float horizontalInput;
    private float verticaInput;
    private bool isBreaking;

    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = massOfCar; //set car mass
        rb.centerOfMass = new Vector3(0, centerOfMass, 0); //Set center of mess
    }
    
    private void Update()
    {
        GetInput();
    }
        
    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    protected virtual void GetInput()
    {
        //Get input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticaInput = Input.GetAxisRaw("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }
    
    protected Wheels GetWheel(WheelsPosition wheelPosition)
    {
        //Get wheels position
        foreach (var wheel in wheels)
        {
            if (wheel.WheelsPosition == wheelPosition)
            {
                return wheel;
            }
        }
        return default(Wheels);
    }
    
    private void HandleMotor()
    {
        //Apply front and rear motor force
        GetWheel(WheelsPosition.FrontLeft).WheelCollider.motorTorque = verticaInput * FrontWheelMotorForce;
        GetWheel(WheelsPosition.FrontRight).WheelCollider.motorTorque = verticaInput * FrontWheelMotorForce;
        GetWheel(WheelsPosition.RearLeft).WheelCollider.motorTorque = verticaInput * RearWheelMotorForce;
        GetWheel(WheelsPosition.RearRight).WheelCollider.motorTorque = verticaInput * RearWheelMotorForce;

        //Apply breaking system
        if (isBreaking)
        {
            currentbreakForce = BreakForce;
            ApplyBreaking();
        }
        else
        {
            currentbreakForce = 0f;
            ApplyBreaking();
            // ApplyBreaking();
        }
    }
    
    private void ApplyBreaking()
    {
        //TODO:[Drift]Implement next time [maybe this can ref the sideways function
        WheelHit ground;
        GetWheel(WheelsPosition.FrontLeft).WheelCollider.GetGroundHit(out ground);
        GetWheel(WheelsPosition.FrontRight).WheelCollider.GetGroundHit(out ground);
        GetWheel(WheelsPosition.RearLeft).WheelCollider.GetGroundHit(out ground);
        GetWheel(WheelsPosition.RearRight).WheelCollider.GetGroundHit(out ground);
        //TODO:[Drift]
            
        //Break system
        GetWheel(WheelsPosition.RearLeft).WheelCollider.brakeTorque = currentbreakForce;
        GetWheel(WheelsPosition.RearRight).WheelCollider.brakeTorque = currentbreakForce;
    }
    
    private void HandleSteering()
    {
        //Set front wheels turn 
        currentsteerAngle = maxSteeringAngle * horizontalInput;
        GetWheel(WheelsPosition.FrontLeft).WheelCollider.steerAngle = currentsteerAngle; 
        GetWheel(WheelsPosition.FrontRight).WheelCollider.steerAngle = currentsteerAngle;
    }
    
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        //Get position and rotation of wheel collider the world
        wheelCollider.GetWorldPose(out var position, out var rotation);
        wheelTransform.rotation = rotation;
        wheelTransform.position = position;
    }
    
    private void UpdateWheels()
    {
        //Animate wheels model
        foreach (var wheel in wheels)
        {
            UpdateSingleWheel(wheel.WheelCollider, wheel.WheelModel);
        }
    }
}
