using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AircraftControls : MonoBehaviour
{
    #region References

    private Rigidbody jetRigid;

    #endregion

    #region Empty variables

    [HideInInspector]
    public float pitchValue;
    [HideInInspector]
    public float rollValue;
    [HideInInspector]
    public float rudderValue;
    [HideInInspector]
    public float currentSpeed;

    #endregion

    #region Pitch, roll and rudder variables

    [Header("Control variables")]
    [Tooltip("This adjusts how fast you roll. A higher value will cause you to roll quicker.")]
    public float rollForce = 2f;
    [Tooltip("This adjusts how fast you pitch. A higher value will cause you to pitch up and down quicker.")]
    public float pitchForce = 1.6f;
    [Tooltip("This adjusts how fast the rudder moves the plane left and right. A higher value will cause you to turn quicker.")]
    public float rudderForce = 2f;

    [Space(20)]

    #endregion

    #region Forces

    [Header("Force adaption")]
    [Tooltip("You shouldn't need to adjust this value. This is to offset the weight of the rigidbody so the above values are relatively normal. Lowering this value will cause the plane acceleration to be more sluggish. Speeding it up will accelerate it faster but might cause unrealistic physics and/or a rubber band effect")]
    public float thrustModifier = 10f;
    [Tooltip("You shouldn't need to adjust this value. This is to offset the weight of the rigidbody so the above values are relatively normal. Lowering this value will cause the plane acceleration to be more sluggish. Speeding it up will accelerate it faster but might cause unrealistic physics and/or a rubber band effect")]
    public float pitchModifier = 10000f;
    [Tooltip("You shouldn't need to adjust this value. This is to offset the weight of the rigidbody so the above values are relatively normal. Lowering this value will cause the plane acceleration to be more sluggish. Speeding it up will accelerate it faster but might cause unrealistic physics and/or a rubber band effect")]
    public float rollModifier = 170000f;
    [Tooltip("You shouldn't need to adjust this value. This is to offset the weight of the rigidbody so the above values are relatively normal. Lowering this value will cause the plane acceleration to be more sluggish. Speeding it up will accelerate it faster but might cause unrealistic physics and/or a rubber band effect")]
    public float rudderModifier = 220000f;

    [Space(20)]
    #endregion

    #region Throttle

    [HideInInspector]
    public float throttleValue;

    [Header("Throttle values")]

    public float maxThrottle = 10f;
    public float minThrottle = -2f;
    [Range(0.1f, 5)]
    public float acceleration = 1f;

    [Space(20)]
    #endregion

    #region Taxi settings

    [Header("Taxi Settings")]
    public float taxiRotationSpeed = 0.4f;
    private bool isGrounded = false;

    #endregion

    private void Start()
    {
        jetRigid = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        #region Speed calcs

        currentSpeed = jetRigid.velocity.magnitude;

        #endregion

        #region Inputs

        PitchInput();
        RollInput();
        RudderInput();

        #endregion

        #region Power control

        throttleValue += Input.GetAxis("Vertical") * (acceleration / 100);
        throttleValue = (throttleValue > maxThrottle) ? maxThrottle : throttleValue;
        throttleValue = (throttleValue < minThrottle) ? minThrottle : throttleValue;

        #endregion

        #region Movement Functions

        ForwardThrust();
        PitchControl();
        
        if (!isGrounded)
        {
            RollControl();
            RudderControl();
        }

        #endregion

        #region Taxi

        if (isGrounded)
        {
            TaxiRotate();
        }

        #endregion

    }

    private void ForwardThrust()
    {
        jetRigid.AddForce(transform.forward * throttleValue * thrustModifier, ForceMode.Acceleration);        
    }

    #region Pitch, Roll and Rudder Controls

    private void PitchControl()
    {
        jetRigid.AddTorque(transform.right * pitchValue * pitchModifier * pitchForce, ForceMode.Force);
    }

    private void RollControl()
    {
        jetRigid.AddTorque(transform.forward * -rollValue * rollForce * rollModifier, ForceMode.Force);
    }

    private void RudderControl()
    {
        jetRigid.AddTorque(transform.up * rudderValue * rudderForce * rudderModifier, ForceMode.Force);
    }

    #endregion

    #region Inputs

    private void PitchInput()
    {
        pitchValue = Input.GetAxis("Mouse Y") * throttleValue * 5;
    }

    private void RollInput()
    {
        rollValue = Input.GetAxis("Horizontal");
    }

    private void RudderInput()
    {
        rudderValue = Input.GetAxis("Rudder");
    }

    #endregion

    #region Taxi

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void TaxiRotate()
    {
        Quaternion rotation = Quaternion.Euler(0f, rollValue * taxiRotationSpeed, 0f);
        jetRigid.MoveRotation(jetRigid.rotation * rotation);
    }

    #endregion
}
