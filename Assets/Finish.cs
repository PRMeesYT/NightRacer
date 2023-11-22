using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public int level;

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
            if (level == 1)
                scoreManager.Level1(score);
            else if (level == 2)
                scoreManager.Level2(score);
            else if(level == 3)
                scoreManager.Level3(score);
            else if (level == 4)
                scoreManager.Level4(score);

            FlyingCarMovement flyingCarMovement = other.GetComponent<FlyingCarMovement>();
            flyingCarMovement.canMove = false;

            StartCoroutine(FinishLevel());
        }
    }

    IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(1f);


    }
}
