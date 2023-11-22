using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMultiplayer : MonoBehaviour
{
    public bool finished;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!finished)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                UIGame UI = FindObjectOfType<UIGame>();
                UI.Player1Wins();

                FlyingCarMovement flyingCarMovement = other.GetComponent<FlyingCarMovement>();
                flyingCarMovement.canMove = false;
                finished = true;
            }
            else if (other.gameObject.CompareTag("Player2"))
            {
                UIGame UI = FindObjectOfType<UIGame>();
                UI.Player2Wins();

                FlyingCarMovement flyingCarMovement = other.GetComponent<FlyingCarMovement>();
                flyingCarMovement.canMove = false;
                finished = true;
            }
        }
    }

    IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(1f);


    }
}
