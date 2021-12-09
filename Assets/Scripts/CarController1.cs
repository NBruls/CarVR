using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CarController1 : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed;
    public float turnSpeed;
    public float gravityMultiplier;
    public float maxspeed;
    public bool KeyboardInput;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(KeyboardInput)
        {
            KeyboardMove();
            KeyboardTurn();
        }
        else
        {
            VRMove();
            VRTurn();
        }
        Fall();
    }

    void KeyboardMove() 
    {
        if(Input.GetKey(KeyCode.UpArrow)) 
        {
            _rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * speed * 10);
        }
        else if(Input.GetKey(KeyCode.DownArrow)) 
        {
            _rb.AddRelativeForce(new Vector3(-Vector3.forward.x, 0, -Vector3.forward.z) * speed * 10);
        }
        Vector3 localVelocity = transform.InverseTransformDirection(_rb.velocity);
        localVelocity.x = 0;
        //localVelocity.z = Math.Min(localVelocity.z, maxspeed);
        _rb.velocity = transform.TransformDirection(localVelocity);
    }

    void VRMove() 
    {
        if(OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger)) 
        {
            _rb.AddRelativeForce(new Vector3(Vector3.forward.x, 0, Vector3.forward.z) * speed * 10);
        }
        else if(OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)) 
        {
            _rb.AddRelativeForce(new Vector3(-Vector3.forward.x, 0, -Vector3.forward.z) * speed * 10);
        }
        Vector3 localVelocity = transform.InverseTransformDirection(_rb.velocity);
        localVelocity.x = 0;
        //localVelocity.z = Math.Min(localVelocity.z, maxspeed);
        _rb.velocity = transform.TransformDirection(localVelocity);
    }

    void KeyboardTurn() 
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            _rb.AddTorque(Vector3.up * turnSpeed * 10);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            _rb.AddTorque(-Vector3.up * turnSpeed * 10);
        }
    }

    void VRTurn() 
    {
        if(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x > 0)
        {
            _rb.AddTorque(Vector3.up * turnSpeed * 10);
        }
        else if(OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x < 0)
        {
            _rb.AddTorque(-Vector3.up * turnSpeed * 10);
        }
    }

    void Fall() 
    {
        _rb.AddForce(Vector3.down * gravityMultiplier * 10);
    }
}
