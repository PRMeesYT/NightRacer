using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMultiplayer : MonoBehaviour
{
    public bool finished;

    public FlyingCarMovement player;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Finish(int player)
    {
        if (!finished)
        {
            if (player == 1)
            {
                UIGame UI = FindObjectOfType<UIGame>();
                UI.Player1Wins();

                finished = true;
            }
            else if (player == 2)
            {
                UIGame UI = FindObjectOfType<UIGame>();
                UI.Player2Wins();

                finished = true;
            }
        }
    }

    IEnumerator FinishLevel()
    {
        yield return new WaitForSeconds(1f);


    }
}
