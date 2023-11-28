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

                    FlyingCarMovement flyingCarMovement1 = GetComponent<FlyingCarMovement>();

                    if (lapsCompleted >= lapsToComplete)
                    {
                        FlyingCarMovement flyingCarMovement = GetComponent<FlyingCarMovement>();
                        int player = flyingCarMovement.player;

                        RaceManager raceManager = FindObjectOfType<RaceManager>();
                        if (raceManager.multiplayer)
                        {
                            FinishMultiplayer finish = FindObjectOfType<FinishMultiplayer>();
                            finish.Finish(player);
                        }
                    }
                    else
                    {
                        StartCoroutine(nextLapText(flyingCarMovement1.player));
                    }
                }

                OnPassCheckpoint.Invoke(this);
            }
        }
    }

    IEnumerator nextLapText(int player)
    {
        UIGame UI = FindObjectOfType<UIGame>();
        if (UI.Player1Text != null)
        {

            if (player == 1)
            {
                if (lapsCompleted == lapsToComplete - 1)
                {
                    UI.Player1Text.text = "Final Lap";
                }
                else
                {
                    UI.Player1Text.text = "Lap " + (lapsCompleted + 1).ToString();
                }
            }
            else if (player == 2)
            {
                if (lapsCompleted == lapsToComplete - 1)
                {
                    UI.Player2Text.text = "Final Lap";
                }
                else
                {
                    UI.Player2Text.text = "Lap " + (lapsCompleted + 1).ToString();
                }
            }

            yield return new WaitForSeconds(2f);

            if (player == 1)
            {
                UI.Player1Text.text = "";
            }
            else if (player == 2)
            {
                UI.Player2Text.text = "";
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
