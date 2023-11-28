using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DisplayLapTimes : MonoBehaviour
{
    public ScoreManager lapTimeManager;
    public TMP_Text lapTimesText;

    void Start()
    {
        if (lapTimeManager == null)
        {
            Debug.LogError("LapTimeManager reference not set!");
            return;
        }

        if (lapTimesText == null)
        {
            Debug.LogError("TMP Text reference not set!");
            return;
        }

        DisplaySavedLapTimes();
    }

    void DisplaySavedLapTimes()
    {
        List<float> lapTimes = lapTimeManager.GetSavedLapTimes();

        string lapTimesString = "Saved Lap Times:\n";

        for (int i = 0; i < lapTimes.Count; i++)
        {
            lapTimesString += $"Lap {i + 1}: {lapTimes[i]:F2} seconds\n";
        }

        lapTimesText.text = lapTimesString;
    }
}
