using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float goldTime;
    public float silverTime;
    public float bronzeTime;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Level1(float time)
    {
        if (time <= goldTime)
        {
            Debug.Log("Gold " + time);
        }
        else if (time > goldTime && time <= silverTime)
        {
            Debug.Log("Silver " + time);
        }
        else if(time > silverTime && time <= bronzeTime)
        {
            Debug.Log("Bronze " + time);
        }
        else
        {
            Debug.Log(time);
        }
    }

    public void Level2(float time)
    {
        if (time <= goldTime)
        {
            Debug.Log("Gold " + time);
        }
        else if (time > goldTime && time <= silverTime)
        {
            Debug.Log("Silver " + time);
        }
        else if (time > silverTime && time <= bronzeTime)
        {
            Debug.Log("Bronze " + time);
        }
        else
        {
            Debug.Log(time);
        }
    }

    public void Level3(float time)
    {
        if (time <= goldTime)
        {
            Debug.Log("Gold " + time);
        }
        else if (time > goldTime && time <= silverTime)
        {
            Debug.Log("Silver " + time);
        }
        else if (time > silverTime && time <= bronzeTime)
        {
            Debug.Log("Bronze " + time);
        }
        else
        {
            Debug.Log(time);
        }
    }

    public void Level4(float time)
    {
        if (time <= goldTime)
        {
            Debug.Log("Gold " + time);
        }
        else if (time > goldTime && time <= silverTime)
        {
            Debug.Log("Silver " + time);
        }
        else if (time > silverTime && time <= bronzeTime)
        {
            Debug.Log("Bronze " + time);
        }
        else
        {
            Debug.Log(time);
        }
    }
}
