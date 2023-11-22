using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingCarMovement : MonoBehaviour
{
    public float power;
    public float maxAngle;

    private float horizontal;
    private float vertical;

    public List<GameObject> springs;

    public GameObject cm;
    public GameObject prop;

    public bool canMove;

    public float player;

    Rigidbody rb;

    UIGame UI;

    void Start()
    {
        //UI = FindObjectOfType<UIGame>();
        //UI.GetComponents();

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = cm.transform.localPosition;

        canMove = false;
    }

    void Update()
    {
        if (canMove && player == 1)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }
        else if (canMove && player == 2)
        {
            vertical = Input.GetAxis("Vertical2");
            horizontal = Input.GetAxis("Horizontal2");
        }
        rb.AddForceAtPosition(Time.deltaTime * transform.TransformDirection(Vector3.forward) * vertical * 400f, prop.transform.position);
        rb.AddTorque(Time.deltaTime * transform.TransformDirection(Vector3.up) * horizontal * 300f);
        foreach (GameObject spring in springs)
        {
            RaycastHit hit;
            if (Physics.Raycast(spring.transform.position, transform.TransformDirection(Vector3.down), out hit, 3f))
            {
                rb.AddForceAtPosition(Time.deltaTime * transform.TransformDirection(Vector3.up) * Mathf.Pow(3f - hit.distance, 2) / 3f * 250f, spring.transform.position);
            }
        }

        rb.AddForce(-Time.deltaTime * transform.TransformDirection(Vector3.right) * transform.InverseTransformVector(rb.velocity).x * 5f);
    }

    public void DisableCollision()
    {

    }
}
