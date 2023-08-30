using UnityEngine;
using System.Collections;

public class FrameRateOptimization : MonoBehaviour
{
    private float timer;
    private bool firstTime = true;

    private void displayFPS()
    {
        Debug.Log(1 / Time.deltaTime);
    }

    private void Update()
    {
        if(timer>Time.deltaTime * 2)
        {
            if(firstTime)
            {
                Application.targetFrameRate = 60;
                firstTime = false;
            }
        }
        else
        {
            timer++;
        }
    }


}