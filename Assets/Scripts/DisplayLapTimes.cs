using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DisplayLapTimes : MonoBehaviour
{
    public ScoreManager lapTimeManager;
    public TextMeshProUGUI lapTimesText;

    void Start()
    {
        lapTimeManager = FindObjectOfType<ScoreManager>();

        // Ensure that the LapTimeManager reference is set in the Inspector
        if (lapTimeManager == null)
        {
            Debug.LogError("LapTimeManager reference not set!");
            return;
        }

        lapTimesText = GetComponent<TextMeshProUGUI>();

        // Ensure that the TMP Text reference is set in the Inspector
        if (lapTimesText == null)
        {
            Debug.LogError("TMP Text reference not set!");
            return;
        }

        // Display saved lap times
        DisplaySavedLapTimes();
    }

    void DisplaySavedLapTimes()
    {
        // Get the saved lap times using LapTimeManager
        List<float> lapTimes = lapTimeManager.GetSavedLapTimes();

        // Construct a string to display all lap times
        string lapTimesString = "Saved Lap Times:\n";

        for (int i = 0; i < lapTimes.Count; i++)
        {
            lapTimesString += $"Lap {i + 1}: {lapTimes[i]:F2} seconds\n";
        }

        // Update the TMP Text component with the lap times string
        lapTimesText.text = lapTimesString;
    }
}
