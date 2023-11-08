using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    public TextMeshProUGUI speedText;

    float speed;

    CarMovement car;

    void Start()
    {
        car = FindAnyObjectByType<CarMovement>();
    }

    void Update()
    {
        speed = car.rb.velocity.magnitude * 3.6f;
        speedText.text = (int)speed + " kp/h";
    }
}
