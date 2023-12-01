using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RaceManager : MonoBehaviour
{
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;

    public TextMeshProUGUI UIText;

    public Transform[] startLocation;

    public GameObject[] hoverCar;
    int carSelected;

    [SerializeField] private List<Transform> carTransfromList;

    private List<CheckpointSingle> checkpointSingleList;
    private List<int> nextCheckpointSingleIndexList;

    public GameObject finish;

    bool finished;

    public bool multiplayer;

    public FlyingCarMovement flyingCarMovement;
    public FlyingCarMovement flyingCarMovementPlayer2;

    [Space(10)]

    // These Static Variables are accessed in "checkpoint" Script
    public Transform[] checkPointArray;
    public static Transform[] checkpointA;
    public static int currentCheckpoint = 0;
    public static int currentLap = 0;
    public Vector3 startPos;
    public int Lap;

    //StartLight
    enum TrafficLights { Off, Red, Yellow, Green };

    private TrafficLights trafficLigts;


    [SerializeField] private AudioClip trafficSoundSFX;

    public UIGame UI;

    CarShowCaseManager carShowCaseManager;
    CameraController camController;
    CameraController camController2;

    private void Awake()
    {
        carTransfromList = new List<Transform>();

        GameObject car1 = Instantiate(hoverCar[carSelected], startLocation[0]);

        flyingCarMovement = car1.GetComponent<FlyingCarMovement>();

        flyingCarMovement.player = 1;

        carTransfromList.Add(car1.transform);

        camController = GameObject.Find("Player1 Virtual Camera").GetComponent<CameraController>();
        camController.SetupCamera();

        if (multiplayer)
        {
            GameObject car2 = Instantiate(hoverCar[carSelected], startLocation[1]);

            car2.tag = "Player2";

            flyingCarMovementPlayer2 = car2.GetComponent<FlyingCarMovement>();

            flyingCarMovementPlayer2.player = 2;

            carTransfromList.Add(car2.transform);

            camController2 = GameObject.Find("Player2 Virual Camera").GetComponent<CameraController>();
            camController2.SetupCamera2();

            startPos = transform.position;
            currentCheckpoint = 0;
            currentLap = 0;
        }

        if (!multiplayer)
        {
            #region
            //Transform checkpointTransform = transform.Find("CheckPoints");

            //checkpointSingleList = new List<CheckpointSingle>();
            //foreach (Transform checkPointSingleTransform in checkpointTransform)
            //{
            //    CheckpointSingle checkpointSingle = checkPointSingleTransform.GetComponent<CheckpointSingle>();

            //    checkpointSingle.SetTrackCheckpoints(this);

            //    checkpointSingleList.Add(checkpointSingle);
            //}

            //nextCheckpointSingleIndexList = new List<int>();
            //foreach (Transform carTransform in carTransfromList)
            //{
            //    nextCheckpointSingleIndexList.Add(0);
            //}
            #endregion
            startPos = transform.position;
            currentCheckpoint = 0;
            currentLap = 0;
        }


        UI = FindObjectOfType<UIGame>();
    }

    #region
    public void CarThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform)
    {
        if (!finished)
        {
            int nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransfromList.IndexOf(carTransform)];
            if (checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
            {
                //Correct Checkpoint
                Debug.Log("Correct");
                CheckpointSingle correctCheckpointSingle = checkpointSingleList[nextCheckpointSingleIndex];
                correctCheckpointSingle.Hide();

                nextCheckpointSingleIndex = nextCheckpointSingleIndexList[carTransfromList.IndexOf(carTransform)] = (nextCheckpointSingleIndex + 1);
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

            if (nextCheckpointSingleIndex == checkpointSingleList.Count)
            {
                Debug.Log("test");
                finish.SetActive(true);
                finished = true;
            }

            Debug.Log(nextCheckpointSingleIndex);
        }
    }
    #endregion

    public void ResetCar(FlyingCarMovement flyingCar)
    {
        StartCoroutine(OutofBounce(flyingCar));
    }

    IEnumerator OutofBounce(FlyingCarMovement flyingCar)
    {
        yield return new WaitForSeconds(.1f);
        Transform spawnPosition = flyingCar.gameObject.GetComponent<CarLapCounter>().GetCheckpointLocation();

        if (spawnPosition == null)
        {
            spawnPosition = startLocation[0];
        }

        flyingCar.gameObject.transform.position = spawnPosition.position;
        flyingCar.gameObject.transform.rotation = Quaternion.Euler(0, spawnPosition.rotation.y, 0);
        flyingCar.rb.velocity = Vector3.zero;
    }

    void Start()
    {

    }

    void Update()
    {
        Debug.Log(carSelected);

        if (multiplayer)
        {
            Lap = currentLap;
            checkpointA = checkPointArray;
        }
        else
        {
            return;
        }
    }

    public void SetCarSelected(int number)
    {
        carSelected = number;
    }
}
