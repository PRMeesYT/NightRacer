using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private RaceManager raceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FlyingCarMovement>(out FlyingCarMovement flyingCar))
        {
            raceManager.PlayerThroughCheckpoint(this);
        }
    }

    public void SetTrackCheckpoints(RaceManager raceManager)
    {
        this.raceManager = raceManager;
    }
}
