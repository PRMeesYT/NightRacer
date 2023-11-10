using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCarMovement : MonoBehaviour
{
    public float power;
    public float maxAngle;

    private float _forward;
    private float _backward;
    private float _up;
    private float _strafe;
    private float _angle;
    private float _brake;

    public List<GameObject> springs;

    public GameObject cm;
    public GameObject prop;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = cm.transform.localPosition;
    }

    void Update()
    {
        rb.AddForceAtPosition(Time.deltaTime * transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * 400f, prop.transform.position);
        rb.AddTorque(Time.deltaTime * transform.TransformDirection(Vector3.up) * Input.GetAxis("Horizontal") * 300f);
        foreach (GameObject spring in springs)
        {
            RaycastHit hit;
            if (Physics.Raycast(spring.transform.position, transform.TransformDirection(Vector3.down), out hit, 3f))
            {
                rb.AddForceAtPosition(Time.deltaTime * transform.TransformDirection(Vector3.up) * Mathf.Pow(3f - hit.distance, 2) / 3f * 250f, spring.transform.position);
            }
            Debug.Log(hit.distance);
        }

        rb.AddForce(-Time.deltaTime * transform.TransformDirection(Vector3.right) * transform.InverseTransformVector(rb.velocity).x * 5f);

        //_forward = Input.GetAxis("Fire1");
        //_backward = Input.GetAxis("Fire2");
        //_up = Input.GetAxis("Vertical");
        //_strafe = Input.GetAxis("Horizontal");

        //Movement();
    }

    public void Movement()
    {
        if (_forward > 0)
        {
            transform.position += Vector3.forward * power * Time.deltaTime;
        }

        if (_backward > 0)
        {
            transform.position += Vector3.back * power * Time.deltaTime;
        }

        if (_strafe < 0)
        {
            transform.position += Vector3.left * power * Time.deltaTime;
        }

        if (_strafe > 0)
        {
            transform.position += Vector3.right * power * Time.deltaTime;
        }

        if (_up < 0)
        {
            transform.position += Vector3.down * power * Time.deltaTime;
        }

        if (_up > 0)
        {
            transform.position += Vector3.up * power * Time.deltaTime;
        }
    }
}
