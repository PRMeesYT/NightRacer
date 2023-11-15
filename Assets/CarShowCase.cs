using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShowCase : MonoBehaviour
{
    public float speed;

    public Transform carSpawnPoint;

    void Start()
    {
        transform.position = carSpawnPoint.position;
    }

    void Update()
    {
        transform.Rotate(0f, 1f * speed * Time.deltaTime, 0f, Space.Self);
    }
}
