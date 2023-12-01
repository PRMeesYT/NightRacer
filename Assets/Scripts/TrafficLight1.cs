using System.Collections;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    enum TrafficLights { Off, Red, Yellow, Green };

    private TrafficLights trafficLigts;

    private bool gameStarted = false;

    [SerializeField] SkinnedMeshRenderer greenTrafficLight;
    [SerializeField] SkinnedMeshRenderer yellowTrafficLight;
    [SerializeField] SkinnedMeshRenderer redTrafficLight;
    [SerializeField] SkinnedMeshRenderer OffTrafficLight;

    [SerializeField] private AudioClip trafficSoundSFX;

    RaceManager raceManager;

    private void Awake()
    {
        greenTrafficLight = GameObject.Find("Traffic Light Green").GetComponent<SkinnedMeshRenderer>();
        yellowTrafficLight = GameObject.Find("Traffic Light Oranje").GetComponent<SkinnedMeshRenderer>();
        redTrafficLight = GameObject.Find("Traffic Light Red").GetComponent<SkinnedMeshRenderer>();
        OffTrafficLight = GameObject.Find("Traffic Light Gray").GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        raceManager = FindObjectOfType<RaceManager>();
        raceManager.UIText.text = "";

        greenTrafficLight.enabled = false;
        yellowTrafficLight.enabled = false;
        redTrafficLight.enabled = false;
        OffTrafficLight.enabled = true;

        StartCoroutine(StartTrafficLights());
    }

    private void Update()
    {
        if (trafficLigts == TrafficLights.Off)
        {
            greenTrafficLight.enabled = false;
            yellowTrafficLight.enabled = false;
            redTrafficLight.enabled = false;
            OffTrafficLight.enabled = true;
        }
        if (trafficLigts == TrafficLights.Green)
        {
            greenTrafficLight.enabled = true;
            yellowTrafficLight.enabled = false;
            redTrafficLight.enabled = false;
            OffTrafficLight.enabled = false;
        }
        if (trafficLigts == TrafficLights.Yellow)
        {
            greenTrafficLight.enabled = false;
            yellowTrafficLight.enabled = true;
            redTrafficLight.enabled = false;
            OffTrafficLight.enabled = false;
        }
        if (trafficLigts == TrafficLights.Red)
        {
            greenTrafficLight.enabled = false;
            yellowTrafficLight.enabled = false;
            redTrafficLight.enabled = true;
            OffTrafficLight.enabled = false;
        }
    }

    IEnumerator StartTrafficLights()
    {
        yield return new WaitForSeconds(1f);
        trafficLigts = TrafficLights.Red;
        yield return new WaitForSeconds(1f);
        trafficLigts = TrafficLights.Yellow;
        yield return new WaitForSeconds(1f);
        trafficLigts = TrafficLights.Green;
        raceManager.flyingCarMovement.canMove = true;
        if (raceManager.flyingCarMovementPlayer2 != null)
            raceManager.flyingCarMovementPlayer2.canMove = true;
        if (raceManager.multiplayer == false)
            raceManager.UI.startTimer = true;
        yield return new WaitForSeconds(4f);
        Destroy(this);
    }
}
