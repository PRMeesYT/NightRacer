using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManagerUI : MonoBehaviour
{
    [SerializeField] private RaceManager checkPoints;

    void Start()
    {
        checkPoints.OnPlayerCorrectCheckpoint += RaceManager_OnPlayerCorrectCheckpoint;
        checkPoints.OnPlayerWrongCheckpoint += RaceManager_OnPlayerWrongCheckpoint;

        Hide();
    }

    private void RaceManager_OnPlayerCorrectCheckpoint(object sender, System.EventArgs e)
    {
        Show();
    }

    private void RaceManager_OnPlayerWrongCheckpoint(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
