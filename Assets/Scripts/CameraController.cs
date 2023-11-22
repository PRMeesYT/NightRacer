using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    public bool playerSpawned;

    public int cam;

    Transform player;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (playerSpawned)
        {
            virtualCamera.LookAt = player;
            virtualCamera.Follow = player;
        }
    }

    public void SetupCamera()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerSpawned = true;
    }

    public void SetupCamera2()
    {
            player = GameObject.FindGameObjectWithTag("Player2").transform;
            playerSpawned = true;
    }
}
