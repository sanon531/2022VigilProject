using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System.Linq;
public class VigilMasterMind : Agent, IVigilAction
{

    // Start is called before the first frame update
    [SerializeField]
    List<MovingBot> vigils;
    [SerializeField]
    List<Transform> testTargets;
    Dictionary<Transform, MovingBot> targetDic = new Dictionary<Transform, MovingBot>();

    public void Start()
    {
        foreach (var i in testTargets) 
            targetDic.Add(i, null);
    }


    public void GetResultFromBot(Transform transform, MovingBot bot)
    {
        SetNextTargetToVigil(bot);
    }
    public void DeleteTransform(Transform transform)
    {
        if (testTargets.Contains(transform))
            testTargets.Remove(transform);
    }


    float timer = 0;
    [SerializeField]
    float tick = 0.5f;
    //거리를 어떻게 처리할까.
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > tick)
        {
            for (int i = 0; i < vigils.Count; i++)
                SetNextTargetToVigil(vigils[i]);
            timer = 0;
        }

    }


    public void SetNextTargetToVigil(MovingBot movingBot)
    {
        if (testTargets.Count != 0)
        {
            var target = testTargets.OrderBy(go => (movingBot.transform.position - go.transform.position).sqrMagnitude).First().transform;
            movingBot.MoveToTarget(target);
        }
        else
        {
            movingBot.MovetoOrigin();
        }
    }
}