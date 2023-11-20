using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private GameManager gameManager;
    private List<Action> powerUps = new List<Action>();
    public bool speedBoostActive = false;
    public bool antiCollisionActive = false;
    public bool coinMagnetActive = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        powerUps.Add(SpeedBoost);
        powerUps.Add(AntiCollision);
        //powerUps.Add(CoinMagnet);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        ActivateRandomPowerUp();
        gameManager.PowerPickupSound();
    }

    private void ActivateRandomPowerUp()
    {
        int randomIndex = UnityEngine.Random.Range(0, powerUps.Count);
        powerUps[randomIndex].Invoke();
    }

    private void SpeedBoost()
    {
        ActivateSpeedBoost();
    }

    private void AntiCollision()
    {
        ActivateAntiCollision();
    }

    private void CoinMagnet()
    {
        ActivateCoinMagnet();
    }

    IEnumerator ActivateSpeedBoost()
    {
        speedBoostActive = true;
        yield return new WaitForSeconds(3f);
        speedBoostActive = false;
    }

    IEnumerator ActivateAntiCollision()
    {
        antiCollisionActive = true;
        yield return new WaitForSeconds(3f);
        antiCollisionActive = false;
    }
    IEnumerator ActivateCoinMagnet()
    {
        coinMagnetActive = true;
        yield return new WaitForSeconds(3f);
        coinMagnetActive = false;
    }
}
