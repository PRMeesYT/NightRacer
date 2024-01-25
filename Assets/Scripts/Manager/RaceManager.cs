using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    // Events
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;

    // UI Text
    public TextMeshProUGUI uiText;

    // Start locations for players
    public Transform[] startLocations;

    // Hover cars and selected car index
    public GameObject[] hoverCars;
    private int _carSelected;

    // Lists for cars, checkpoints, and their indices
    private List<Transform> _carTransformList;
    private List<CheckpointSingle> _checkpointSingleList;
    private List<int> _nextCheckpointSingleIndexList;

    // Finish line
    public GameObject finish;
    private bool _finished;

    // Multiplayer flag
    public bool multiplayer;

    // Player movements
    public FlyingCarMovement flyingCarMovement;
    public FlyingCarMovement flyingCarMovementPlayer2;

    // Static variables accessed in the "Checkpoint" script
    [Space(10)]
    public Transform[] checkPointArray;
    public static Transform[] CheckpointA;
    public static int CurrentCheckpoint;
    public static int CurrentLap = 0;
    public Vector3 startPos;
    public int lap;

    // Audio
    [SerializeField] private AudioClip trafficSoundSfx;

    // UI Game
    public UIGame ui;

    // Managers and controllers
    private CarShowCaseManager _carShowCaseManager;
    private CameraController _camController;
    private CameraController _camController2;

    private void Awake()
    {
        // Initialize lists
        _carTransformList = new List<Transform>();
        _checkpointSingleList = new List<CheckpointSingle>();
        _nextCheckpointSingleIndexList = new List<int>();

        // Instantiate and set up the first car
        GameObject car1 = GameObject.Find("Car 1");
        flyingCarMovement = car1.GetComponent<FlyingCarMovement>();
        _carTransformList.Add(car1.transform);
        
        _camController = GameObject.Find("Player1 Virtual Camera").GetComponent<CameraController>();
        _camController.SetupCamera();

        // Set up the second car and camera if multiplayer
        if (multiplayer)
        {
            GameObject car2 = GameObject.Find("Car 2");
            flyingCarMovementPlayer2 = car2.GetComponent<FlyingCarMovement>();
            _carTransformList.Add(car2.transform);

            _camController2 = GameObject.Find("Player2 Virtual Camera").GetComponent<CameraController>();
            _camController2.SetupCamera2();

            startPos = transform.position;
            CurrentCheckpoint = 0;
            CurrentLap = 0;
        }

        // Set up initial values if not multiplayer
        if (!multiplayer)
        {
            startPos = transform.position;
            CurrentCheckpoint = 0;
            CurrentLap = 0;
        }

        // Find UI Game object
        ui = FindObjectOfType<UIGame>();
    }

    // Handle the car passing through a checkpoint
    public void CarThroughCheckpoint(CheckpointSingle checkpointSingle, Transform carTransform)
    {
        if (_finished)
            return;
        
        var nextCheckpointSingleIndex = _nextCheckpointSingleIndexList[_carTransformList.IndexOf(carTransform)];

        if (_checkpointSingleList.IndexOf(checkpointSingle) == nextCheckpointSingleIndex)
        {
            // Correct Checkpoint
            Debug.Log("Correct");
            CheckpointSingle correctCheckpointSingle = _checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Hide();

            nextCheckpointSingleIndex = _nextCheckpointSingleIndexList[_carTransformList.IndexOf(carTransform)] = (nextCheckpointSingleIndex + 1);
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            // Wrong Checkpoint
            Debug.Log("Wrong");
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);
            CheckpointSingle correctCheckpointSingle = _checkpointSingleList[nextCheckpointSingleIndex];
            correctCheckpointSingle.Show();
        }

        // Check if the race is finished
        if (nextCheckpointSingleIndex == _checkpointSingleList.Count)
        {
            Debug.Log("test");
            finish.SetActive(true);
            _finished = true;
        }

        Debug.Log(nextCheckpointSingleIndex);
    }

    // Reset the car's position if out of bounds
    public void ResetCar(FlyingCarMovement flyingCar)
    {
        StartCoroutine(OutOfBounds(flyingCar));
    }

    // Coroutine to reset the car's position
    public IEnumerator OutOfBounds(FlyingCarMovement flyingCar)
    {
        yield return new WaitForSeconds(0.1f);
        Transform spawnPosition = flyingCar.gameObject.GetComponent<CarLapCounter>().GetCheckpointLocation();

        if (spawnPosition == null)
        {
            spawnPosition = startLocations[0];
        }

        flyingCar.gameObject.transform.position = spawnPosition.position;
        flyingCar.gameObject.transform.rotation = Quaternion.Euler(0, spawnPosition.rotation.y, 0);
        flyingCar.rb.velocity = Vector3.zero;
    }

    // Update method
    void Update()
    {
        if (Input.GetKeyDown("P1_Reset"))
        {
            StartCoroutine(OutOfBounds(flyingCarMovement));
        }
        else if (Input.GetKeyDown("P2_Reset"))
        {
            StartCoroutine(OutOfBounds(flyingCarMovementPlayer2));
        }

        Debug.Log(_carSelected);

        if (multiplayer)
        {
            lap = CurrentLap;
            CheckpointA = checkPointArray;
        }
        else
        {
            return;
        }
    }

    // Set the selected car
    public void SetCarSelected(int number)
    {
        _carSelected = number;
    }
}
