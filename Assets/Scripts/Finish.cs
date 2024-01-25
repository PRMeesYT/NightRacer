using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public int level;

    public GameObject medal;

    public Sprite goldMedal;
    public Sprite silverMedal;
    public Sprite bronzeMedal;
    public Sprite none;

    private MoveToNextLevel nextLevel;

    void Start()
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

            if (SceneManager.GetActiveScene().buildIndex == nextLevel.eindLevel)
            {
                Debug.Log("You Completed all Levels");

                SceneManager.LoadScene("StartScene");
            }
            else
            {
                SceneManager.LoadScene(nextLevel.nextSceneLoad);

                if (nextLevel.nextSceneLoad > PlayerPrefs.GetInt("levelAt"))
                {
                    PlayerPrefs.SetInt("levelAt", nextLevel.nextSceneLoad);
                }
            }
        }
    }

    public void StartFinish(int score)
    {
        StartCoroutine(FinishLevel(score));
    }

    public IEnumerator FinishLevel(int score)
    {
        yield return new WaitForSeconds(3f);
        Transform UI = FindObjectOfType<UIGame>().transform;
        GameObject medalInstantiate = Instantiate(medal, UI);
        Image medalImage = medalInstantiate.GetComponent<Image>();
        if (score == 1)
        {
            medalImage.sprite = goldMedal;
        }
        else if (score == 2)
        {
            medalImage.sprite = silverMedal;
        }
        else if (score == 3)
        {
            medalImage.sprite = bronzeMedal;
        }
        else
        {
            Destroy(medalImage.gameObject);
        }
    }
}
