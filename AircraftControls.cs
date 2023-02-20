using JDTechnology;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AircraftControls : MonoBehaviour
{
    #region Aircraft Profiles

    [SerializeField]
    private AircraftProfiles aircraftProfile;

    #endregion

    #region References

    private Rigidbody jetRigid;

    #endregion

    #region Player Input

    private float pitchValue;
    public float PitchValue
    {
        get { return pitchValue; }
        set { pitchValue = value; }
    }


    private float rollValue;
    public float RollValue
    {
        get { return rollValue; }
        set { rollValue = value; }
    }


    private float rudderValue;
    public float RudderValue
    {
        get { return rudderValue; }
        set { rudderValue = value; }
    }


    private float currentSpeed;
    public float CurrentSpeed
    {
        get { return currentSpeed; }
        set { currentSpeed = value; }
    }

    #endregion

    #region Forces

    /// <summary>
    /// You shouldn't need to adjust the following values. 
    /// This is to offset the weight of the rigidbody so the above values are relatively normal. 
    /// Lowering these values will cause the plane acceleration to be more sluggish. 
    /// Upping them will accelerate it faster BUT MIGHT cause unrealistic physics 
    /// and/or a rubber band effect
    /// </summary>
    private float thrustModifier = 10f;
    private float pitchModifier = 10000f;
    private float rollModifier = 170000f;
    private float rudderModifier = 220000f;

    #endregion

    #region Throttle

    /// <summary>
    /// This is the main throttle control. It acts like a real plane throttle.
    /// "W" will increase it slowly, and "S" will decrease it slowly.
    /// </summary>
    
    private float throttleValue;
    public float ThrottleValue
    {
        get { return throttleValue; }
        set { throttleValue = value; }
    }

    #endregion

    #region Taxi

    private bool isGrounded = false;

    #endregion

    private void Start()
    {
        jetRigid = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        #region Speed calcs

        CurrentSpeed = jetRigid.velocity.magnitude;

        #endregion

        #region Inputs

        PitchInput();
        RollInput();
        RudderInput();

        #endregion

        #region Power control

        ThrottleValue += Input.GetAxis("Vertical") * (aircraftProfile.acceleration / 100);
        ThrottleValue = (ThrottleValue > aircraftProfile.maxThrottle) ? aircraftProfile.maxThrottle : ThrottleValue;
        ThrottleValue = (ThrottleValue < aircraftProfile.minThrottle) ? aircraftProfile.minThrottle : ThrottleValue;

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
        jetRigid.AddForce(transform.forward * ThrottleValue * thrustModifier, ForceMode.Acceleration);        
    }

    #region Pitch, Roll and Rudder Controls

    private void PitchControl()
    {
        jetRigid.AddTorque(transform.right * PitchValue * pitchModifier * aircraftProfile.pitchForce, ForceMode.Force);
    }

    private void RollControl()
    {
        jetRigid.AddTorque(transform.forward * -RollValue * aircraftProfile.rollForce * rollModifier, ForceMode.Force);
    }

    private void RudderControl()
    {
        jetRigid.AddTorque(transform.up * RudderValue * aircraftProfile.rudderForce * rudderModifier, ForceMode.Force);
    }

    #endregion

    #region Inputs

    private void PitchInput()
    {
        PitchValue = Input.GetAxis("Mouse Y") * ThrottleValue * 5;
    }

    private void RollInput()
    {
        RollValue = Input.GetAxis("Horizontal");
    }

    private void RudderInput()
    {
        RudderValue = Input.GetAxis("Rudder");
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
        Quaternion rotation = Quaternion.Euler(0f, RollValue * aircraftProfile.taxiRotationSpeed, 0f);
        jetRigid.MoveRotation(jetRigid.rotation * rotation);
    }

    #endregion
}
