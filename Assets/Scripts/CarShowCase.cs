using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarShowCase : MonoBehaviour
{
    public float speed;

    public Transform carSpawnPoint;

    Quaternion rotation;

    void Start()
    {
        rotation = Quaternion.Euler(0, 180, 0);
        transform.position = carSpawnPoint.position;
        transform.rotation = rotation;
    }

    void Update()
    {
        transform.Rotate(0f, 1f * speed * Time.deltaTime, 0f, Space.Self);
    }

    private void OnEnable()
    {
        transform.rotation = rotation;
    }
}
