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
        
    }

    void Update()
    {
        if (car != null)
        {
            speed = carRigidbody.velocity.magnitude * 3.6f;
            speedText.text = (int)speed + " kp/h";
        }
    }

    public void GetComponents()
    {
        car = GameObject.FindGameObjectWithTag("Player").gameObject;
        carRigidbody = car.GetComponent<Rigidbody>();
    }
}
