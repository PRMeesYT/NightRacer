using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool isPowered;
    public bool canTurn;

    public Transform _visual;

    public WheelCollider _wheelCollider;

    void Start()
    {
        _wheelCollider = GetComponent<WheelCollider>();
    }

    void Update()
    {
        //Vector3 pos;
        //Quaternion rotation;
        //_wheelCollider.GetWorldPose(out pos, out rotation);
        //_visual.transform.position = pos;
        //_visual.transform.rotation = rotation;
    }

    public void Accelerate(float power)
    {
        if (isPowered)
            _wheelCollider.motorTorque = power;
    }

    public void Turn(float angle)
    {
        if (canTurn)
            _wheelCollider.steerAngle = angle;
    }

    public void Brake(float power)
    {
        _wheelCollider.brakeTorque = power;
    }
}
