using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{
    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckpoint = 0f;

    int numberOfPassedCheckpoints = 0;

    int lapsCompleted = 0;
    public int lapsToComplete = 2;

    public event Action<CarLapCounter> OnPassCheckpoint;

    Transform checkpointPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Checkpoint checkPoint = other.GetComponent<Checkpoint>();

            if (passedCheckPointNumber + 1 == checkPoint.checkPointNumber)
            {
                checkpointPosition = checkPoint.transform;

                passedCheckPointNumber = checkPoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                timeAtLastPassedCheckpoint = Time.time;

                if (checkPoint.isFinish)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    if (lapsCompleted >= lapsToComplete)
                    {
                        FlyingCarMovement flyingCarMovement = GetComponent<FlyingCarMovement>();
                        int player = flyingCarMovement.player;

                        FinishMultiplayer finish = FindObjectOfType<FinishMultiplayer>();
                        finish.Finish(player);
                    }
                }


                OnPassCheckpoint.Invoke(this);
            }
        }
    }

    public Transform GetCheckpointLocation()
    {
        if (checkpointPosition == null)
        {
            return null;
        }
        else
        {
            return checkpointPosition;
        }
    }
}
