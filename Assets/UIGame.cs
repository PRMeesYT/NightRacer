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

    Rigidbody carRigidbody;

    void Start()
    {
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
        if (car != null)
        {
            speed = carRigidbody.velocity.magnitude * 3.6f;
            speedText.text = (int)speed + " kp/h";
        }
    }

    public void GetComponents()
    {
        car = GameObject.FindGameObjectWithTag("Player").gameObject;
        carRigidbody = car.GetComponent<Rigidbody>();
    }
}
