using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarShowCaseManager : MonoBehaviour
{
    public Transform carSpawnPoint;

    public GameObject[] carModels;

    public bool[] carUnlocked = new bool[4];

    public int points;

    [SerializeField] private TextMeshProUGUI pointsText;

    void Start()
    {
        pointsText.text = "Points: " + points;
        carUnlocked[0] = true;
        foreach (GameObject model in carModels)
        {
            model.SetActive(false);
        }
    }

    void Update()
    {

    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Car1()
    {
        if (carUnlocked[0] == true)
        {
            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[0].SetActive(true);
        }
    }

    public void Car2()
    {
        if (carUnlocked[1] == true)
        {
            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[1].SetActive(true);
        }
        else if (points > 0)
        {
            points--;
            carUnlocked[1] = true;
            pointsText.text = "Points: " + points;

            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[1].SetActive(true);
        }
    }

    public void Car3()
    {
        if (carUnlocked[2] == true)
        {
            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[2].SetActive(true);
        }
        else if (points > 0)
        {
            points--;
            carUnlocked[2] = true;
            pointsText.text = "Points: " + points;

            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[2].SetActive(true);
        }
    }

    public void Car4()
    {
        if (carUnlocked[3] == true)
        {
            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[3].SetActive(true);
        }
        else if (points > 0)
        {
            points--;
            carUnlocked[3] = true;
            pointsText.text = "Points: " + points;

            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[3].SetActive(true);
        }
    }
}
