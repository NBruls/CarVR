using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableController : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log(other);
        if(other.CompareTag("PlayerHand")) {
            Debug.Log("hand triggered this...");
        }
    }
}
