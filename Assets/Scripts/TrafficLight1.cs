using UnityEngine;
using System.Collections;

public class TrafficLight : MonoBehaviour
{
    enum TrafficLights { Off, Red, Yellow, Green };

    private TrafficLights trafficLigts;

    FlyingCarMovement[] flyingCarMovement;
    [SerializeField] SkinnedMeshRenderer greenTrafficLight;
    [SerializeField] SkinnedMeshRenderer yellowTrafficLight;
    [SerializeField] SkinnedMeshRenderer redTrafficLight;
    [SerializeField] SkinnedMeshRenderer OffTrafficLight;

    [SerializeField] private AudioClip trafficSoundSFX;

    private void Start()
    {
        flyingCarMovement = FindObjectsOfType<FlyingCarMovement>();
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
        else if (trafficLigts == TrafficLights.Green)
        {
            greenTrafficLight.enabled = true;
            yellowTrafficLight.enabled = false;
            redTrafficLight.enabled = false;
            OffTrafficLight.enabled = false;
        }
        else if (trafficLigts == TrafficLights.Yellow)
        {
            greenTrafficLight.enabled = false;
            yellowTrafficLight.enabled = true;
            redTrafficLight.enabled = false;
            OffTrafficLight.enabled = false;
        }
        else if (trafficLigts == TrafficLights.Red)
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
        AudioManager.Instance.PlaySFX(trafficSoundSFX, 1);
        yield return new WaitForSeconds(.5f);
        trafficLigts = TrafficLights.Red;
        yield return new WaitForSeconds(1f);
        trafficLigts = TrafficLights.Yellow;
        yield return new WaitForSeconds(1f);

        foreach (FlyingCarMovement car in flyingCarMovement)
        {
            car.canMove = true;
        }

        UIGame timer = FindObjectOfType<UIGame>();
        timer.startTimer = true;

        trafficLigts = TrafficLights.Green;
    }
}
