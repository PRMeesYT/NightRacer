using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float goldTime;
    public float silverTime;
    public float bronzeTime;

    int score;

    private const string lapTimesKey = "SavedLapTimes";

    private void Start()
    {
        SaveLapTime(100);
    }

    public void SaveLapTime(float lapTime)
    {
        List<float> lapTimes = new List<float>();

        if (PlayerPrefs.HasKey(lapTimesKey))
        {
            string[] lapTimesString = PlayerPrefs.GetString(lapTimesKey).Split(';');

            foreach (string time in lapTimesString)
            {
                if (!string.IsNullOrEmpty(time))
                {
                    lapTimes.Add(float.Parse(time));
                }
            }
        }

        lapTimes.Add(lapTime);
        string lapTimesToSave = string.Join(";", lapTimes);

        PlayerPrefs.SetString(lapTimesKey, lapTimesToSave);
        PlayerPrefs.Save();
    }

    public List<float> GetSavedLapTimes()
    {
        List<float> lapTimes = new List<float>();

        if (PlayerPrefs.HasKey(lapTimesKey))
        {
            string[] lapTimesString = PlayerPrefs.GetString(lapTimesKey).Split(';');

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
        else if(time > silverTime && time <= bronzeTime)
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
