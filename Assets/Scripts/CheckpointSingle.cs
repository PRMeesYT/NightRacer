using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    private RaceManager raceManager;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        Hide();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FlyingCarMovement>(out FlyingCarMovement flyingCar))
        {
            raceManager.CarThroughCheckpoint(this, other.transform);
        }
    }

    public void SetTrackCheckpoints(RaceManager raceManager)
    {
        this.raceManager = raceManager;
    }

    public void Show()
    {
        meshRenderer.enabled = true;
    }

    public void Hide()
    {
        meshRenderer.enabled = false;
    }
}
