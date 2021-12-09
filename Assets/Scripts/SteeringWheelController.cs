using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SteeringWheelController : MonoBehaviour
{
    public GameObject rightHand;
    private Transform _rightHandOriginalParent;
    private bool _rightHandOnWheel = false;

    public GameObject leftHand;
    private Transform _leftHandOriginalParent;
    private bool _leftHandOnWheel = false;

    public Transform[] snapPositionsTransform;
    // private GameObject[] _snapPositions;
    // private List<Transform> _snapPositionsTransform;

    public GameObject vehicle;
    private Rigidbody _vehicleRigidBody;

    public float currentSteeringWheelRotation;

    private float _turnDampening = 250;

    public Transform directionalObject;
    
    // Start is called before the first frame update
    void Start()
    {
    //     _snapPositions = GameObject.FindGameObjectsWithTag("SnapPosition");
    //     _snapPositionsTransform = _snapPositions.Select(sp => sp.transform).ToList();

        _vehicleRigidBody = vehicle.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ReleaseHandFromWheel();
        
        ConvertHandRotationToSteeringWheelRotation();

        TurnVehicle();

        currentSteeringWheelRotation = -transform.rotation.eulerAngles.z;
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("PlayerHand")) {
            if(_rightHandOnWheel == false && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch)) {
                PlaceHandOnWheel(ref rightHand, ref _rightHandOriginalParent, ref _rightHandOnWheel);
            }

            if(_leftHandOnWheel == false && OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch)) {
                PlaceHandOnWheel(ref leftHand, ref _leftHandOriginalParent, ref _leftHandOnWheel);
            }
        }
    }

    private void PlaceHandOnWheel(ref GameObject hand, ref Transform originalParent, ref bool handOnWheel) {
        var shortestDistance = Vector3.Distance(snapPositionsTransform[0].position, hand.transform.position);
        var bestSnap = snapPositionsTransform[0];
        foreach(var snapPosition in snapPositionsTransform) {
            if (snapPosition.childCount == 0) {
                var distance = Vector3.Distance(snapPosition.position, hand.transform.position);
                if(distance < shortestDistance) {
                    shortestDistance = distance;
                    bestSnap = snapPosition;
                }
            }
        }
        originalParent = hand.transform.parent;

        hand.transform.parent = bestSnap.transform;
        hand.transform.position = bestSnap.transform.position;

        handOnWheel = true;
    }

    private void ReleaseHandFromWheel() {
        if(_rightHandOnWheel && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch)) {
            rightHand.transform.parent = _rightHandOriginalParent;
            rightHand.transform.position = _rightHandOriginalParent.position;
            rightHand.transform.rotation = _rightHandOriginalParent.rotation;
            _rightHandOnWheel = false;
        }

        if(_leftHandOnWheel && OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch)) {
            leftHand.transform.parent = _leftHandOriginalParent;
            leftHand.transform.position = _leftHandOriginalParent.position;
            leftHand.transform.rotation = _leftHandOriginalParent.rotation;
            _leftHandOnWheel = false;
        }

        if(!_rightHandOnWheel && !_leftHandOnWheel) {
            transform.parent = null;
        }
    }

    private void ConvertHandRotationToSteeringWheelRotation() {
        if(_rightHandOnWheel && !_leftHandOnWheel) {
            Quaternion newRot = Quaternion.Euler(0,0,_rightHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.rotation = newRot;
            transform.parent = directionalObject;
        } else if (!_rightHandOnWheel && _leftHandOnWheel) {
            Quaternion newRot = Quaternion.Euler(0,0,_leftHandOriginalParent.transform.rotation.eulerAngles.z);
            directionalObject.rotation = newRot;
            transform.parent = directionalObject;
        } else if (_rightHandOnWheel && _leftHandOnWheel) {
            Quaternion newRotRight = Quaternion.Euler(0,0,_rightHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion newRotLeft = Quaternion.Euler(0,0,_rightHandOriginalParent.transform.rotation.eulerAngles.z);
            Quaternion finalRot = Quaternion.Slerp(newRotRight, newRotLeft, 1.0f / 2.0f);
            directionalObject.rotation = finalRot;
            transform.parent = directionalObject;
        }
    }

    private void TurnVehicle() {
        var turn = -transform.rotation.eulerAngles.z;
        if(turn < -350) {
            turn += 360;
        }
        _vehicleRigidBody.MoveRotation(Quaternion.RotateTowards(vehicle.transform.rotation, Quaternion.Euler(0,turn,0), Time.deltaTime * _turnDampening));
    }
}
