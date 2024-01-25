using System;
using TMPro;
using UnityEngine;

public class UIGame : MonoBehaviour
{
    public TextMeshProUGUI speedText;

    private float _speed;

    public GameObject car;

    [SerializeField] private RaceManager checkPoints;

    [SerializeField] private GameObject wrongDir;

    public bool startTimer;

    public float currentTimer;
    public bool countDown;

    [SerializeField] private Camera p1Cam;
    [SerializeField] private Camera p2Cam;

    public TextMeshProUGUI player1Text;
    public TextMeshProUGUI player2Text;

    private Rigidbody _carRigidbody;
    private RaceManager _raceManager;

    private void Awake()
    {
        _raceManager = FindObjectOfType<RaceManager>();
    }

    void Start()
    {
        if (player1Text != null)
        {
            player1Text.text = String.Empty;
            player2Text.text = String.Empty;
        }


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
            _speed = _carRigidbody.velocity.magnitude * 3.6f;
            speedText.text = (int)_speed + " kp/h";
        }
    }

    private void TrackTimer()
    {
        currentTimer = countDown ? currentTimer -= Time.deltaTime : currentTimer += Time.deltaTime;
    }

    public void GetComponents()
    {
        car = GameObject.FindGameObjectWithTag("Player").gameObject;
        _carRigidbody = car.GetComponent<Rigidbody>();
    }

    public void Finish()
    {
        EventManager.OnTimerStop();

        _raceManager.uiText.text = "Finish!!!";
    }

    public void Player1Wins()
    {
        _raceManager.uiText.text = "Player1 Wins!!!";
    }

    public void Player2Wins()
    {
        _raceManager.uiText.text = "Player2 Wins!!!";
    }
}
