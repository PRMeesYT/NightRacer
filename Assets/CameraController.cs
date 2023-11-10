using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraLookatObject;
    [Range(0, 20)] public float smoothTime = 5;

    void Start()
    {
        
    }

    void Update()
    {
        CameraBehaviour();
    }

    private void CameraBehaviour()
    {
        Vector3 velocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(cameraLookatObject.transform.position, cameraLookatObject.transform.position, ref velocity, smoothTime * Time.deltaTime);
        transform.LookAt(cameraLookatObject.transform);
    }
}
