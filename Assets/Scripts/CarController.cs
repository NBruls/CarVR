using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public bool KeyboardControlled;
    public float speed;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyboardControlled) 
        {
            UpdateKeyboard();
        }
    }

    void UpdateKeyboard() 
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 direction = transform.forward*speed*Time.deltaTime;
            Debug.Log(direction);
            _rb.AddForce(direction, ForceMode.Force);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 direction = -transform.forward * speed * Time.deltaTime;
            Debug.Log(direction);
            _rb.AddForce(direction,ForceMode.Force);
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 direction = -transform.right * speed * Time.deltaTime;
            Debug.Log(direction);
            _rb.AddForce(direction,ForceMode.Force);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 direction = transform.right * speed * Time.deltaTime;
            Debug.Log(direction);
            _rb.AddForce(direction,ForceMode.Force);
        }
    }
}
