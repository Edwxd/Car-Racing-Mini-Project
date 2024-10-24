using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarConstroller : MonoBehaviour
{
    /*
        Rigidbody rb;

        [SerializeField]
        float accelaration = 5f;
        [SerializeField]
        float steeringWheelPower = 5f;
        float steeringAmout, speed, direction;

        private void Start()
        {
            rb = GetComponent<Rigidbody> ();
        }


        private void FixedUpdate()
        {
            steeringAmout = -Input.GetAxis("Horizontal");

            speed = Input.GetAxis("Vertical") * acceleration;

            // Determine the sign of the dot product between the velocity and the relative point velocity
            float dotProduct = Vector2.Dot(rb.velocity, rb.GetRelativePointVelocity(Vector2.up));
            direction = Mathf.Sign(dotProduct);

            // Rotate the Rigidbody based on steering amount, steering power, velocity magnitude, and direction
            rb.rotation += steeringAmout * steeringWheelPower * rb.velocity.magnitude * direction;

            // Apply force in the forward direction based on speed
            rb.AddRelativeForce(Vector2.up * speed);

            // Apply sideways force based on steering amount
            rb.AddRelativeForce(-Vector2.right * rb.velocity.magnitude * steeringAmout / 2);
        }

    */






    //[Header("Car Settings")]
    [SerializeField]
    private float accelerationFactor = 10f; //How fast the car will accelarate
    [SerializeField]
    private float turnFactor = 3.5f; // How fast the car will turn
    [SerializeField]
    private float drifFactor = 0.95f; // How much the car can drift
    [SerializeField]
    private float maxSpeed = 5.0f; // How fast the car can go
    [SerializeField]
    private float velocityVsUp = 0; // How fast the car can go




    //Local variable
    float accelarationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    //Component
    Rigidbody2D carRigidBody2D;


    private void Awake()
    {
        carRigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }


    void Update()
    {

    }


    private void FixedUpdate()
    {
        ApplyEngineForce();

        ApplySteering();
    }

    private void ApplySteering()
    {

        float minimumFactorSpeed = (carRigidBody2D.velocity.magnitude / 8);
        minimumFactorSpeed = Mathf.Clamp01(minimumFactorSpeed);


        //updating rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minimumFactorSpeed;

        //rotate the car object
        carRigidBody2D.MoveRotation(rotationAngle);
    }

    private void ApplyEngineForce()
    {
        // Apply drag if there is no user input in order to sto the car from going forward
        if (accelarationInput == 0)
        {
            carRigidBody2D.drag = Mathf.Lerp(carRigidBody2D.drag, 3.0f, Time.fixedDeltaTime * 3);

        }
        else
        {
            carRigidBody2D.drag = 0;
        }

        //create engine force
        Vector2 engineForceVector = (transform.up * accelarationInput * accelerationFactor) / 3;

        // Make the car go forward by applying force to the rigide body
        carRigidBody2D.AddForce(engineForceVector, ForceMode2D.Force);





        //This caculates how much the car's velicty is aligned with the "up" direction
        velocityVsUp = Vector2.Dot(transform.up, carRigidBody2D.velocity);

        //This is a limite set in order not to go past the speed limite
        if (velocityVsUp > maxSpeed && accelarationInput > 0)
        {
            return;
        }

        //This method will cut in half the speed limite while going backwards
        if (velocityVsUp < -maxSpeed * 0.5f && accelarationInput < 0)
        {
            return;
        }

        //Limite so we can't go faster in any direction while accelarating
        if (carRigidBody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelarationInput > 0)
        {
            return;
        }


    }


    void ApplyBreak()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Apply braking force
            float brakeForce = 100f;
            carRigidBody2D.AddForce(-carRigidBody2D.velocity.normalized * brakeForce, ForceMode2D.Force);
        }
    }
    

    void removeSideVelocity()
    {
        //Calculate the dot product of both values 
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidBody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidBody2D.velocity, transform.right);

        carRigidBody2D.velocity = forwardVelocity + rightVelocity * drifFactor;//This wll remove one part of the right velocity in order for the car to drift correctly 
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelarationInput = inputVector.y;
    }

}
