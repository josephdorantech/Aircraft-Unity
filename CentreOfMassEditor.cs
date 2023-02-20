using UnityEngine;

namespace JDTechnology
{
    [RequireComponent(typeof(Rigidbody))]
    public class CentreOfMassEditor : MonoBehaviour
    {
        private Rigidbody rb;
        [SerializeField]
        private Vector3 centreOfGravity;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            rb.centerOfMass = centreOfGravity;
        }

    }
}
