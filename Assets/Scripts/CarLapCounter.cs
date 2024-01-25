using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{
    private int _passedCheckPointNumber = 0;
    private float _timeAtLastPassedCheckpoint = 0f;

    private int _numberOfPassedCheckpoints = 0;

    private int _lapsCompleted = 0;
    public int lapsToComplete = 2;

    public event Action<CarLapCounter> OnPassCheckpoint;

    private Transform _checkpointPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Checkpoint checkPoint = other.GetComponent<Checkpoint>();

            if (_passedCheckPointNumber + 1 != checkPoint.checkPointNumber) 
                return;
            
            _checkpointPosition = checkPoint.transform;

            _passedCheckPointNumber = checkPoint.checkPointNumber;

            _numberOfPassedCheckpoints++;

            _timeAtLastPassedCheckpoint = Time.time;

            if (checkPoint.isFinish)
            {
                _passedCheckPointNumber = 0;
                _lapsCompleted++;

                FlyingCarMovement flyingCarMovement1 = GetComponent<FlyingCarMovement>();

                if (_lapsCompleted >= lapsToComplete)
                {
                    FlyingCarMovement flyingCarMovement = GetComponent<FlyingCarMovement>();
                    int player = (int)flyingCarMovement.belongsTo + 1;

                    RaceManager raceManager = FindObjectOfType<RaceManager>();
                    if (raceManager.multiplayer)
                    {
                        FinishMultiplayer finish = FindObjectOfType<FinishMultiplayer>();
                        finish.Finish(player);
                    }
                }
                else
                {
                    StartCoroutine(NextLapText((int)flyingCarMovement1.belongsTo + 1));
                }
            }

            if (OnPassCheckpoint != null)
                OnPassCheckpoint.Invoke(this);
        }
    }

    IEnumerator NextLapText(int player)
    {
        UIGame ui = FindObjectOfType<UIGame>();
        if (ui.player1Text != null)
        {

            if (player == 1)
            {
                if (_lapsCompleted == lapsToComplete - 1)
                {
                    ui.player1Text.text = "Final Lap";
                }
                else
                {
                    ui.player1Text.text = "Lap " + (_lapsCompleted + 1).ToString();
                }
            }
            else if (player == 2)
            {
                if (_lapsCompleted == lapsToComplete - 1)
                {
                    ui.player2Text.text = "Final Lap";
                }
                else
                {
                    ui.player2Text.text = "Lap " + (_lapsCompleted + 1).ToString();
                }
            }

            yield return new WaitForSeconds(2f);

            if (player == 1)
            {
                ui.player1Text.text = "";
            }
            else if (player == 2)
            {
                ui.player2Text.text = "";
            }
        }

    }

    public Transform GetCheckpointLocation()
    {
        if (_checkpointPosition == null)
        {
            return null;
        }
        else
        {
            return _checkpointPosition;
        }
    }
}
