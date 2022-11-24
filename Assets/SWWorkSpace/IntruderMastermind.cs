using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class IntruderMastermind : MonoBehaviour, IVigilAction
{

    [SerializeField]
    List<MovingBot> intruders;

    [SerializeField]
    List<Transform> testTargets;


    [SerializeField]
    bool isdebug = true;

    // Start is called before the first frame update
    void Start()
    {
        if (!isdebug)
            return;

        for (int i =0; i < intruders.Count;i++) 
        {
            intruders[i].MoveToTarget(testTargets[i]);
        }
    }

    // Update is called once per frame

    float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5f) 
        {
            for (int i = 0; i < intruders.Count; i++)
            {
                intruders[i].MoveToTarget(testTargets[i]);
            }
            timer = 0f;
        }


    }

    public void GetResultFromBot(Transform transform, MovingBot bot)
    {


    }
    public void DeleteTransform(Transform transform)
    {
    }
}