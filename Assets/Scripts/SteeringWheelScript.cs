using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelScript : MonoBehaviour
{
    public Transform target;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.MoveRotation(target.transform.rotation);
    }
}
