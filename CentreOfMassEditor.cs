using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
