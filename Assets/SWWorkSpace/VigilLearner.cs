using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class VigilLearner : Agent, IVigilLearn
{

    // Start is called before the first frame update
    [SerializeField]
    List<MovingBot> vigils;

    public void GetResultFromBot()
    {



    }
    public override void CollectObservations(VectorSensor sensor)
    {
    }




    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float initialVal = actionBuffers.ContinuousActions[0];


    }


    // Update is called once per frame
    void Update()
    {

    }
}
