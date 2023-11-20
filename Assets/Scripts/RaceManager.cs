using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class RaceManager : MonoBehaviour
{
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;

    public Transform[] startLocation;

    public GameObject hoverCar;

    [SerializeField] private List<Transform> carTransfromList;

    private List<CheckpointSingle> checkpointSingleList;
    private List<int> nextCheckpointSingleIndexList;

    public GameObject finish;

    public bool multiplayer;

    public TextMeshProUGUI UIText;

    [SerializeField] private FlyingCarMovement flyingCarMovement;
    [SerializeField] private FlyingCarMovement flyingCarMovementPlayer2;
    UIGame UI;
    CameraController camController;

    private void Awake()
    {
        carTransfromList = new List<Transform>();

        GameObject car1 = Instantiate(hoverCar, startLocation[0]);

        flyingCarMovement = car1.GetComponent<FlyingCarMovement>();

        flyingCarMovement.player = 1;

        carTransfromList.Add(car1.transform);

        camController = FindObjectOfType<CameraController>();
        camController.SetupCamera();

        if (multiplayer)
        {
            GameObject car2 = Instantiate(hoverCar, startLocation[1]);

            car2.tag = "Player2";

            flyingCarMovementPlayer2 = car2.GetComponent<FlyingCarMovement>();

            flyingCarMovementPlayer2.player = 2;

            carTransfromList.Add(car2.transform);
        }


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
        StartCoroutine(CountDown());
    }

    public void CarThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform)
    {
        int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransfromList.IndexOf(carTransform)];
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

    IEnumerator CountDown()
    {
        float timer = 3f;
        UIText.text = "3";
        yield return new WaitForSeconds(1f);
        timer--;
        UIText.text = "2";
        yield return new WaitForSeconds(1f);
        timer--;
        UIText.text = "1";
        yield return new WaitForSeconds(1f);
        UIText.text = "GO";
        flyingCarMovement.canMove = true;
        if (flyingCarMovementPlayer2 != null)
            flyingCarMovementPlayer2.canMove = true;
        yield return new WaitForSeconds(1f);
        UIText.text = "";
        UI.startTimer = true;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
