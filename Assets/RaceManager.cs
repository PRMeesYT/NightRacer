using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public Transform startLocation;

    public GameObject hoverCar;

    public CinemachineVirtualCamera virtualCamera;


    private void Awake()
    {
        Instantiate(hoverCar, startLocation);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
