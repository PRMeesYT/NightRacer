using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float goldTime;
    public float silverTime;
    public float bronzeTime;

    int score;

    private const string lapTimesKey = "SavedLapTimes";

    public void SaveLapTime(float lapTime)
    {
        List<float> lapTimes = new List<float>();

        // Check if lap times exist in PlayerPrefs
        if (PlayerPrefs.HasKey(lapTimesKey))
        {
            string[] lapTimesString = PlayerPrefs.GetString(lapTimesKey).Split(';');

            // Convert string values to floats and add to the list
            foreach (string time in lapTimesString)
            {
                if (!string.IsNullOrEmpty(time))
                {
                    lapTimes.Add(float.Parse(time));
                }
            }
        }

        // Add the new lap time to the list
        lapTimes.Add(lapTime);

        // Convert the list of lap times to a delimited string
        string lapTimesToSave = string.Join(";", lapTimes);

        // Save the lap times to PlayerPrefs
        PlayerPrefs.SetString(lapTimesKey, lapTimesToSave);
        PlayerPrefs.Save();
    }

    public List<float> GetSavedLapTimes()
    {
        List<float> lapTimes = new List<float>();

        // Check if lap times exist in PlayerPrefs
        if (PlayerPrefs.HasKey(lapTimesKey))
        {
            string[] lapTimesString = PlayerPrefs.GetString(lapTimesKey).Split(';');

            // Convert string values to floats and add to the list
            foreach (string time in lapTimesString)
            {
                if (!string.IsNullOrEmpty(time))
                {
                    lapTimes.Add(float.Parse(time));
                }
            }
        }

        return lapTimes;
    }

    public void Level1(float time)
    {
        if (time <= goldTime)
        {
            Debug.Log("Gold " + time);
            score = 1;
        }
        else if (time > goldTime && time <= silverTime)
        {
            Debug.Log("Silver " + time);
            score = 2;
        }
        else if (time > silverTime && time <= bronzeTime)
        {
            Debug.Log("Bronze " + time);
            score = 3;
        }
        else
        {
            Debug.Log(time);
        }

        SaveLapTime(time);

        Finish finish = FindObjectOfType<Finish>();

        finish.StartFinish(score);
    }

    public void Level2(float time)
    {
        if (time <= goldTime)
        {
            Debug.Log("Gold " + time);
        }
        else if (time > goldTime && time <= silverTime)
        {
            Debug.Log("Silver " + time);
        }
        else if (time > silverTime && time <= bronzeTime)
        {
            Debug.Log("Bronze " + time);
        }
        else
        {
            Debug.Log(time);
        }

        SaveLapTime(time);
    }

    public void Level3(float time)
    {
        if (time <= goldTime)
        {
            Debug.Log("Gold " + time);
        }
        else if (time > goldTime && time <= silverTime)
        {
            Debug.Log("Silver " + time);
        }
        else if (time > silverTime && time <= bronzeTime)
        {
            Debug.Log("Bronze " + time);
        }
        else
        {
            Debug.Log(time);
        }

        SaveLapTime(time);
    }

    public void Level4(float time)
    {
        if (time <= goldTime)
        {
            Debug.Log("Gold " + time);
        }
        else if (time > goldTime && time <= silverTime)
        {
            Debug.Log("Silver " + time);
        }
        else if (time > silverTime && time <= bronzeTime)
        {
            Debug.Log("Bronze " + time);
        }
        else
        {
            Debug.Log(time);
        }

        SaveLapTime(time);
    }
}
