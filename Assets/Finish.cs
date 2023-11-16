using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UIGame UI = FindObjectOfType<UIGame>();
            UI.Finish();
            Timer timer = FindObjectOfType<Timer>();
            float score = timer.timeToDisplay;
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.Level1(score);
        }
    }
}
