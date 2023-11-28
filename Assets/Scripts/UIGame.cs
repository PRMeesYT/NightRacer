using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    public TextMeshProUGUI speedText;

    float speed;

    public GameObject car;

    [SerializeField] private RaceManager checkPoints;

    [SerializeField] private GameObject wrongDir;

    public bool startTimer;

    public float currentTimer;
    public bool countDown;

    [SerializeField] private Camera p1Cam;
    [SerializeField] private Camera p2Cam;

    public TextMeshProUGUI Player1Text;
    public TextMeshProUGUI Player2Text;

    Rigidbody carRigidbody;
    RaceManager raceManager;

    private void Awake()
    {
        raceManager = FindObjectOfType<RaceManager>();
    }

    void Start()
    {
        Player1Text.text = "";
        Player2Text.text = "";

        checkPoints.OnPlayerCorrectCheckpoint += RaceManager_OnPlayerCorrectCheckpoint;
        checkPoints.OnPlayerWrongCheckpoint += RaceManager_OnPlayerWrongCheckpoint;

        Hide();
    }

    private void RaceManager_OnPlayerCorrectCheckpoint(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void RaceManager_OnPlayerWrongCheckpoint(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        wrongDir.SetActive(true);
    }

    private void Hide()
    {
        wrongDir.SetActive(false);
    }

    void Update()
    {
        if (startTimer)
        {
            EventManager.OnTimerStart();
            startTimer = false;
        }

        if (car != null)
        {
            speed = carRigidbody.velocity.magnitude * 3.6f;
            speedText.text = (int)speed + " kp/h";
        }
    }

    private void TrackTimer()
    {
        currentTimer = countDown ? currentTimer -= Time.deltaTime : currentTimer += Time.deltaTime;
    }

    public void GetComponents()
    {
        car = GameObject.FindGameObjectWithTag("Player").gameObject;
        carRigidbody = car.GetComponent<Rigidbody>();
    }

    public void Finish()
    {
        EventManager.OnTimerStop();

        raceManager.UIText.text = "Finish!!!";
    }

    public void Player1Wins()
    {
        raceManager.UIText.text = "Player1 Wins!!!";
    }

    public void Player2Wins()
    {
        raceManager.UIText.text = "Player2 Wins!!!";
    }
}
