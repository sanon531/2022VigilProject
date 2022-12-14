using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomLogManager : MonoBehaviour
{
    static CustomLogManager instance = null;

    [SerializeField]
    Text capturedUI, passedUI;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
    }

    int captureCount = 0;
    void CallLog_Captured() 
    {
        capturedUI.text = "Captured : ";
        capturedUI.text += captureCount.ToString();
    }
    public static void Increase_CapturedCount() 
    {
        Debug.Log("CapturedCount");
        instance.captureCount++;
        instance.CallLog_Captured();
    }

    int passedCount = 0;

    void CallLog_Passed()
    {
        passedUI.text = "Passsed : ";
        passedUI.text += passedCount.ToString();
    }
    public static void Increase_PassedCount()
    {
        Debug.Log("PrassedCount");
        instance.passedCount++;
        instance.CallLog_Passed();
    }


}
