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

    public GameObject hoverCar;

    [SerializeField] private List<Transform> carTransfromList;

    private List<CheckpointSingle> checkpointSingleList;
    private List<int> nextCheckpointSingleIndexList;

    public GameObject finish;

    bool finished;

    public bool multiplayer;

    [SerializeField] private FlyingCarMovement flyingCarMovement;
    [SerializeField] private FlyingCarMovement flyingCarMovementPlayer2;

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

    SkinnedMeshRenderer greenTrafficLight;
    SkinnedMeshRenderer yellowTrafficLight;
    SkinnedMeshRenderer redTrafficLight;
    SkinnedMeshRenderer OffTrafficLight;

    [SerializeField] private AudioClip trafficSoundSFX;

    UIGame UI;
    CameraController camController;
    CameraController camController2;

    private void Awake()
    {
        carTransfromList = new List<Transform>();

        GameObject car1 = Instantiate(hoverCar, startLocation[0]);

        flyingCarMovement = car1.GetComponent<FlyingCarMovement>();

        flyingCarMovement.player = 1;

        carTransfromList.Add(car1.transform);

        camController = GameObject.Find("Player1 Virtual Camera").GetComponent<CameraController>();
        camController.SetupCamera();

        if (multiplayer)
        {
            GameObject car2 = Instantiate(hoverCar, startLocation[1]);

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
        StartCoroutine(CountDown());
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

    IEnumerator CountDown()
    {
        UIText.text = "";
        float timer = 3f;
        if (redTrafficLight != null)
            redTrafficLight.enabled = true;

        yield return new WaitForSeconds(1f);
        timer--;

        if (yellowTrafficLight != null)
        {
            yellowTrafficLight.enabled = true;
            redTrafficLight.enabled = false;
        }

        yield return new WaitForSeconds(1f);
        timer--;

        if (greenTrafficLight != null)
        {
            greenTrafficLight.enabled = true;
            yellowTrafficLight.enabled = false;
        }

        flyingCarMovement.canMove = true;
        if (flyingCarMovementPlayer2 != null)
            flyingCarMovementPlayer2.canMove = true;
        if (!multiplayer)
            UI.startTimer = true;
    }

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
        if (greenTrafficLight != null)
        {
            greenTrafficLight.enabled = false;
            yellowTrafficLight.enabled = false;
            redTrafficLight.enabled = false;
        }
    }

    void Update()
    {
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
}
