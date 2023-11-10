using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    public TextMeshProUGUI speedText;

    float speed;

    public GameObject car;

    Rigidbody carRigidbody;

    void Start()
    {
        if (car != null)
        {
            carRigidbody = car.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        speed = carRigidbody.velocity.magnitude * 3.6f;
        speedText.text = (int)speed + " kp/h";
    }
}
