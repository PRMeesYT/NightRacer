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
        transform.position = carSpawnPoint.position;
        rotation = transform.rotation;
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
