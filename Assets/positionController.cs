using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionController : MonoBehaviour
{
    public Transform carPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = carPosition.position;
        transform.rotation = Quaternion.Euler(new Vector3(carPosition.rotation.eulerAngles.x, carPosition.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }
}
