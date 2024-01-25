using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private List<FlyingCarMovement> _cars = new List<FlyingCarMovement>();

    private void Awake()
    {
        _cars.AddRange(FindObjectsOfType<FlyingCarMovement>());
        foreach (var car in _cars)
        {
            car.GetComponent<Rigidbody>().isKinematic = true; // Freeze car initially
        }
    }

    private void Start()
    {
        // Find TextMeshProUGUI in children
        TextMeshProUGUI uiText = GetComponentInChildren<TextMeshProUGUI>();

        if (uiText != null)
        {
            StartCoroutine(StartCountDown(uiText));
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found in children.");
        }
    }

    IEnumerator StartCountDown(TextMeshProUGUI text)
    {
        int countdownDuration = 3;

        // Countdown
        for (int i = countdownDuration; i > 0; i--)
        {
            text.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        // Display "GO!"
        text.text = "GO!";

        foreach (var car in _cars)
        {
            car.GetComponent<Rigidbody>().isKinematic = false; // Unfreeze car after countdown
        }

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
