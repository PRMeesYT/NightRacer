using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;

    public Transform startLocation;

    public GameObject hoverCar;

    [SerializeField] private List<Transform> carTransfromList;

    private List<CheckpointSingle> checkpointSingleList;
    private List<int> nextCheckpointSingleIndexList;

    UIGame UI;

    private void Awake()
    {
        GameObject car1 = Instantiate(hoverCar, startLocation);

        carTransfromList = new List<Transform>();

        carTransfromList.Add(car1.transform);

        Transform checkpointTransform = transform.Find("CheckPoints");
        
        checkpointSingleList = new List<CheckpointSingle>();
        foreach (Transform checkPointSingleTransform in checkpointTransform)
        {
            CheckpointSingle checkpointSingle = checkPointSingleTransform.GetComponent<CheckpointSingle>();

            checkpointSingle.SetTrackCheckpoints(this);

            checkpointSingleList.Add(checkpointSingle);
        }

        nextCheckpointSingleIndexList = new List<int>();
        foreach (Transform carTransform in carTransfromList)
        {
            nextCheckpointSingleIndexList.Add(0);
        }

        UI = FindObjectOfType<UIGame>();
    }

    public void carThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform)
    {
        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransfromList.IndexOf(carTransform)];
        if (checkpointSingleList.IndexOf(checkpointSingle) == 0)
        {
            UI.startTimer = true;
        }
        if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            //Correct Checkpoint
            Debug.Log("Correct");
            CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Hide();

            nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransfromList.IndexOf(carTransform)] = (nextCheckpointSingleIndex + 1) % checkpointSingleList.Count; ;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            //Wrong Checkpoint
            Debug.Log("Wrong");
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
            CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Show();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
