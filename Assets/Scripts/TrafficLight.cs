using UnityEngine;
using System.Collections;
using UnityEngine.Animations;

public class TrafficLight : MonoBehaviour
{
    enum TrafficLights { Off, Red, Yellow, Green};

    private TrafficLights trafficLigts;
    private Animation anim;

    FlyingCarMovement flyingCarMovement;
    SkinnedMeshRenderer greenTrafficLight;
    SkinnedMeshRenderer yellowTrafficLight;
    SkinnedMeshRenderer redTrafficLight;
    SkinnedMeshRenderer OffTrafficLight;

    [SerializeField] private AudioClip trafficSoundSFX;

    private void Start()
    {
        greenTrafficLight.enabled = false;
        yellowTrafficLight.enabled = false;
        redTrafficLight.enabled = false;
        OffTrafficLight.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartTrafficLights());
            Debug.Log("Spatie komt door");
        }

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
        AudioManager.Instance.PlaySFX(trafficSoundSFX, 1);
        yield return new WaitForSeconds(1f);
        trafficLigts = TrafficLights.Red;
        yield return new WaitForSeconds(1f);
        trafficLigts = TrafficLights.Yellow;
        yield return new WaitForSeconds(1f);
        trafficLigts = TrafficLights.Green;
    }

    private void StartGame()
    {
        flyingCarMovement.canMove = true;
        //if (flyingCarMovement.Pl != null)
        //    flyingCarMovementPlayer2.canMove = true;
        //UI.startTimer = true;
    }
}
