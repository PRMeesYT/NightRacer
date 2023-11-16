using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public double goldTime;
    public double silverTime;
    public double bronzeTime;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Level1(double time)
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
}
