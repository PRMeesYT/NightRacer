using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutofBounce : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FlyingCarMovement flyingCarMovement = other.GetComponent<FlyingCarMovement>();
            RaceManager raceManager = FindAnyObjectByType<RaceManager>();
            raceManager.ResetCar(flyingCarMovement);
        }
        else if (other.gameObject.CompareTag("Player2"))
        {
            FlyingCarMovement flyingCarMovement = other.GetComponent<FlyingCarMovement>();
            RaceManager raceManager = FindAnyObjectByType<RaceManager>();
            raceManager.ResetCar(flyingCarMovement);
        }
    }
}
