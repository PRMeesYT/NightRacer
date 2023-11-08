using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public Wheel[] wheels;
    public float power;
    public float maxAngle;
    public float maxSpeed = 50;

    private float _forward;
    private float _angle;
    private float _brake;
    private float _acceleration;

    private bool _drift;

    public Vector3 _centerOfMass;

    public Transform carVisual;

    public TrailRenderer[] trails;

    public GameObject brakeLights;
    public GameObject reverseLights;

    public ParticleSystem[] smoke;

    public Collider groundCheck;
    public LayerMask groundMask;

    public Transform[] shootLocation;
    public GameObject bullet;

    public bool canShoot;
    public bool canMove;

    bool isDriving;

    bool canRotate;

    [SerializeField] private AudioSource accelHigh;
    [SerializeField] private AudioSource decelHigh;
    [SerializeField] private AudioSource decelLow;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        rb.centerOfMass = _centerOfMass;
        canMove = true;
    }

    [System.Obsolete]
    void Update()
    {
        _forward = Input.GetAxis("Vertical");
        _angle = Input.GetAxis("Horizontal");

        _acceleration = Input.GetAxis("Fire1");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _brake = 1;
            //brakeLights.SetActive(true);
        }
        else
        {
            _brake = 0;
            //brakeLights.SetActive(false);
        }

        //if (_forward < 0)
        //{
        //    brakeLights.SetActive(true);
        //    reverseLights.SetActive(true);
        //}
        //else
        //{
        //    reverseLights.SetActive(false);
        //}

        if (_forward != 0.0f)
        {
            //foreach (var particle in smoke)
            //{
            //    particle.startLifetime = 0.7f;
            //}

            float carSpeed = Vector3.Dot(transform.forward, rb.velocity);
        }
        else
        {
            //foreach (var particle in smoke)
            //{
            //    particle.startLifetime = 0;
            //}
        }

        if (_angle < 0.0f)
        {

        }
        else if (_angle > 0.0f)
        {

        }

        if (_forward > 0.0f)
        {
            rb.AddForce(Vector3.forward * power, ForceMode.Acceleration);
        }
        else if (_forward < 0.0f)
        {

        }

        if (rb.velocity.magnitude >= maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    private void FixedUpdate()
    {
        foreach (var wheel in wheels)
        {
            wheel.Accelerate(_forward * power);
            wheel.Turn(_angle * maxAngle);
            wheel.Brake(_brake * power);
        }
    }

    private void CheckDrift()
    {
        if (_drift) { StartEmiter(); }
        else { StopEmiter(); }
    }

    private void StartEmiter()
    {
        foreach (var trail in trails)
        {
            trail.emitting = true;
        }
    }

    private void StopEmiter()
    {
        foreach (var trail in trails)
        {
            trail.emitting = false;
        }
    }

    public bool IsGrounded()
    {
        float extraHeight = .1f;
        RaycastHit hit;
        if (Physics.Raycast(groundCheck.transform.position, Vector3.down, out hit, extraHeight, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
