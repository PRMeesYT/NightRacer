using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarShowCaseManager : MonoBehaviour
{
    public Transform carSpawnPoint;

    public GameObject[] carModels;

    void Start()
    {
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
        foreach (GameObject model in carModels)
        {
            model.SetActive(false);
        }
        carModels[0].SetActive(true);
    }
}
