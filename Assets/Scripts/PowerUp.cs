using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private FlyingCarMovement carMovement;
    private List<Action> powerUps = new List<Action>();
    public bool speedBoostActive = false;
    public bool antiCollisionActive = false;
    public bool coinMagnetActive = false;
    private int powerUpTime = 3;
    private MeshRenderer powerUp;

    private void Start()
    {
        carMovement = FindObjectOfType<FlyingCarMovement>();
        powerUp = GetComponent<MeshRenderer>();
        powerUps.Add(SpeedBoost);
        powerUps.Add(AntiCollision);
        //powerUps.Add(CoinMagnet);
    }

    private void OnTriggerEnter(Collider other)
    {
        ActivateRandomPowerUp();
    }

    private void ActivateRandomPowerUp()
    {
        int randomIndex = UnityEngine.Random.Range(0, powerUps.Count);
        powerUps[randomIndex].Invoke();
    }

    private void SpeedBoost()
    {
        Debug.Log("SpeedBoost activated");
        StartCoroutine(ActivateSpeedBoost());
    }

    private void AntiCollision()
    {
        Debug.Log("AntiCollision activated");
        //StartCoroutine(ActivateAntiCollision());
    }

    private void CoinMagnet()
    {
        Debug.Log("CoinMagnet activated");
        StartCoroutine(ActivateCoinMagnet());
    }

    IEnumerator ActivateSpeedBoost()
    {
        speedBoostActive = true;
        powerUp.enabled = false;
        carMovement.forwardPower += 10;
        Debug.Log("Snelheid: " + carMovement.forwardPower);
        yield return new WaitForSeconds(powerUpTime);
        carMovement.forwardPower -= 10;
        Debug.Log("Snelheid: " + carMovement.forwardPower);
        speedBoostActive = false;
        Destroy(gameObject);
    }

    // IEnumerator ActivateAntiCollision()
    // {
    //     antiCollisionActive = true;
    //     powerUp.enabled = false;
    //     carMovement.ignoreCollision = true;
    //     yield return new WaitForSeconds(powerUpTime);
    //     carMovement.ignoreCollision = false;
    //     antiCollisionActive = false;
    //     Destroy(gameObject);
    // }
    IEnumerator ActivateCoinMagnet()
    {
        coinMagnetActive = true;
        powerUp.enabled = false;
        yield return new WaitForSeconds(powerUpTime);
        coinMagnetActive = false;
        Destroy(gameObject);
    }
}
