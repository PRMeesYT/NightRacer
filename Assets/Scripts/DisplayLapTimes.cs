using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq; // Import LINQ for sorting

public class DisplayLapTimes : MonoBehaviour
{
    public ScoreManager lapTimeManager;
    public TMP_Text lapTimesText;

    void Start()
    {
        // Ensure that the LapTimeManager reference is set in the Inspector
        if (lapTimeManager == null)
        {
            Debug.LogError("LapTimeManager reference not set!");
            return;
        }

        // Ensure that the TMP Text reference is set in the Inspector
        if (lapTimesText == null)
        {
            Debug.LogError("TMP Text reference not set!");
            return;
        }

        // Display saved lap times in order
        DisplaySavedLapTimes();
    }

    void DisplaySavedLapTimes()
    {
        // Get the saved lap times using LapTimeManager
        List<float> lapTimes = lapTimeManager.GetSavedLapTimes();

        // Sort the lap times from fastest to slowest
        lapTimes.Sort();

        // Construct a string to display all lap times
        string lapTimesString = "Saved Lap Times (Fastest to Slowest):\n";

        for (int i = 0; i < lapTimes.Count; i++)
        {
            lapTimesString += $"Lap {i + 1}: {lapTimes[i]:F2} seconds\n";
        }

        // Update the TMP Text component with the lap times string
        lapTimesText.text = lapTimesString;
    }
}
