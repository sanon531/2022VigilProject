using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class Timer : MonoBehaviour
{
    public float LimitTime;
    public Text text_Timer;
    public int maxTime = 60;
    public int iterateCount = 30;

    // Update is called once per frame
    public int timecount=0;


    void Update()
    {
        LimitTime += Time.deltaTime;
        text_Timer.text = "시간 : " + Mathf.Round(LimitTime);
        if (timecount != Mathf.FloorToInt(LimitTime)) 
        {
            timecount = Mathf.FloorToInt(LimitTime);
            CustomLogManager.UpdateGraph(timecount);

            if (timecount > maxTime) 
            {
                timecount = 0;
                CustomLogManager.SaveCall();
            }
        }

    }
}
