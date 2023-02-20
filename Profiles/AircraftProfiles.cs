using UnityEngine;

namespace JDTechnology
{

    [CreateAssetMenu(fileName = "AircraftProfile", menuName = "JDTechnology/AircraftProfile")]
    public class AircraftProfiles : ScriptableObject
    {
        #region Pitch, roll and rudder variables

        [Header("Controls")]
        [Tooltip("This adjusts how fast you roll. A higher value will cause you to roll quicker.")]
        public float rollForce = 2f;
        [Tooltip("This adjusts how fast you pitch. A higher value will cause you to pitch up and down quicker.")]
        public float pitchForce = 1.6f;
        [Tooltip("This adjusts how fast the rudder moves the plane left and right. A higher value will cause you to turn quicker.")]
        public float rudderForce = 2f;

        [Space(20)]

        #endregion

        #region Speed

        [Header("Speed")]

        [Tooltip("This is directly connected to the max speed of the aircraft. Turn this up to achieve a higher speed.")]
        public float maxThrottle = 10f;
        public float minThrottle = -2f;


        [Space(5)]

        [Tooltip("This is how fast the throttle will react. Up this value for smaller jets.")]
        [Range(0.1f, 5)]
        public float acceleration = 1f;


        [Space(20)]
        #endregion

        #region Taxi Settings

        [Header("Taxi Settings")]
        [Tooltip("This is connected to the Roll controls when the aircraft is grounded.")]
        public float taxiRotationSpeed = 0.4f;

        #endregion
    }
}
