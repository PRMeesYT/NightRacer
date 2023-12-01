using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarShowCaseManager : MonoBehaviour
{
    public Transform carSpawnPoint;

    public GameObject[] carModels;

    public bool[] carUnlocked = new bool[4];

    public int points;
    public int carSelected;

    public Image play;

    bool canPlay;
    bool canSelect;

    public bool start;

    [SerializeField] private TextMeshProUGUI pointsText;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }

    void Start()
    {
        pointsText.text = "Points: " + points;
        carUnlocked[0] = true;
        if (carUnlocked != null)
        {
            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
        }

        if (play != null)
        {
            play.color = Color.gray;
        }
    }

    void Update()
    {
        if (start)
        {
            RaceManager raceManager = FindObjectOfType<RaceManager>();
            raceManager.SetCarSelected(carSelected);
            Destroy(gameObject, 5f);
        }
    }

    public void Play()
    {
        if (canPlay)
        {
            start = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Car1()
    {
        if (carUnlocked[0] == true)
        {
            canSelect = true;
            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[0].SetActive(true);
            carSelected = 1;
            canPlay = false;
            play.color = Color.gray;
        }
    }

    public void Car2()
    {
        if (carUnlocked[1] == true)
        {
            canSelect = true;
            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[1].SetActive(true);
            carSelected = 2;
            canPlay = false;
            play.color = Color.gray;
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
            canSelect = true;
            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[2].SetActive(true);
            carSelected = 3;
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
            canSelect = true;
            foreach (GameObject model in carModels)
            {
                model.SetActive(false);
            }
            carModels[3].SetActive(true);
            carSelected = 4;
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

    public void Select()
    {
        if (canSelect)
        {
            canPlay = true;
            play.color = Color.white;
        }
    }
}
