using ChartAndGraph;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomLogManager : MonoBehaviour
{
    static CustomLogManager instance = null;

    [SerializeField]
    Text capturedUI, passedUI;
    [SerializeField]
    Text betweenUI;
    [SerializeField]
    GraphChart graph;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            graph.DataSource.StartBatch(); // start a new update batch
            graph.DataSource.ClearCategory("Passed");
            graph.DataSource.ClearCategory("Catched");
        }
    }
    public static void ResetData() 
    {
        instance.graph.DataSource.StartBatch(); // start a new update batch
        instance.graph.DataSource.ClearCategory("Passed");
        instance.graph.DataSource.ClearCategory("Catched");
        instance.captureCount = 0;
        instance.passedCount = 0;

    }


    int captureCount = 0;
    void CallLog_Captured()
    {
        capturedUI.text = "Captured : ";
        capturedUI.text += captureCount.ToString();
    }
    public static void Increase_CapturedCount()
    {
        instance.captureCount++;
        instance.CallLog_Captured();
    }

    int passedCount = 0;

    void CallLog_Passed()
    {
        passedUI.text = "Passed : ";
        passedUI.text += passedCount.ToString();

    }
    public static void Increase_PassedCount()
    {
        instance.passedCount++;
        instance.CallLog_Passed();
    }
    public static void UpdateGraph(int count) 
    {
        instance.graph.DataSource.AddPointToCategory("Passed", count,instance.passedCount);
        instance.graph.DataSource.AddPointToCategory("Catched", count, instance.captureCount);
        instance.graph.DataSource.EndBatch();
    }


    [SerializeField]
    LogSaver saver;
    public static void SaveCall() 
    {
        instance.saver.JsonSave(instance.passedCount, instance.captureCount);
        ResetData();
        TrainResetter.ResetPlay();
    }

}
