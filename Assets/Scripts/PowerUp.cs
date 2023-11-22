using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private GameManager gameManager;
    private FlyingCarMovement carMovement;
    private List<Action> powerUps = new List<Action>();
    public bool speedBoostActive = false;
    public bool antiCollisionActive = false;
    public bool coinMagnetActive = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        carMovement = FindObjectOfType<FlyingCarMovement>();
        powerUps.Add(SpeedBoost);
        powerUps.Add(AntiCollision);
        //powerUps.Add(CoinMagnet);
    }

    private void OnTriggerEnter(Collider other)
    {
        ActivateRandomPowerUp();
        gameManager.PowerPickupSound();
        Destroy(gameObject);
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
        carMovement.power += 10;
        yield return new WaitForSeconds(3f);
        carMovement.power -= 10;
        speedBoostActive = false;
    }

    IEnumerator ActivateAntiCollision()
    {
        antiCollisionActive = true;
        //carMovement.disableCollision();
        yield return new WaitForSeconds(3f);
        //carMovement.disableCollision();
        antiCollisionActive = false;
    }
    IEnumerator ActivateCoinMagnet()
    {
        coinMagnetActive = true;
        yield return new WaitForSeconds(3f);
        coinMagnetActive = false;
    }
}
