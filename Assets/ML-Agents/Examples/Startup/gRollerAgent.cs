
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;



public class gRollerAgent : Agent
{ 
    Rigidbody rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }
    public GameObject viewModel = null;
    public Transform Target;

    private float time = 0;
    int Pointlast = 0;
    int Point = 0;


    public override void OnEpisodeBegin()
    {
        if (gameObject.tag == "goal")
            return;

         //에피소드 시작시, 포지션 초기화
        if (this.transform.localPosition.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0, 0.5f, 0);
            
        }

        //타겟의 위치를 설정

       
       float rx = 0;
       float rz = 0;
       rx = Random.value * 4 + 2;
       rz = Random.value * 4 + 2;
       Target.localPosition = new Vector3(rx, 0.5f, rz);

       Target.gameObject.SetActive(true);
       Pointlast = 0;
       Point = 0;
   }
    


   
    private void Update()
    {
        time += Time.deltaTime;
        if (time==100)
        {
            EndEpisode();
            time = 0;
        }
    }




    /// 강화학습 행동 결정
    ///

    public float forceMultiplier = 10;

    float m_ForwardSpeed = 1.0f;

    


    public void EnteredTarget()
    {
        Point++;
        CustomLogManager.Increase_CapturedCount();
    }

    public void EnteredGoal()
    {
        AddReward(-5.0f);
        CustomLogManager.Increase_PassedCount();

    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {

      
        
        if (Pointlast < Point)
        {
            AddReward(1.0f);
            Pointlast++;
           
        }
        

        MoveAgent(actionBuffers.DiscreteActions);

     
    }
    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var forwardAxis = act[0];
        var rotateAxis = act[1];

        switch (forwardAxis)
        {
            case 1:
                dirToGo = transform.forward * m_ForwardSpeed;
                break;
        }

        switch (rotateAxis)
        {
            case 1:
                rotateDir = transform.up * -1f;
                break;
            case 2:
                rotateDir = transform.up * 1f;
                break;
        }

        transform.Rotate(rotateDir, Time.deltaTime * 100f);
        rBody.AddForce(dirToGo * forceMultiplier, ForceMode.VelocityChange);


    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut.Clear();
        //Forward
        if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }

        //Rotate
        if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[1] = 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[1] = 2;
        }
    }
        
    
}
