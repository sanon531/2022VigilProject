using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class gTarget : Agent
{
    int Pointlast = 0;
    int Point = 0;

    public Transform Target;
    Rigidbody rBody;
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public float forceMultiplier = 10;

    float m_ForwardSpeed = 1.0f;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Agent")
        {
            gRollerAgent ra = other.GetComponent<gRollerAgent>();
            if (null !=ra)
            {
                ra.EnteredTarget();
                this.gameObject.SetActive(false);
            }
            float rx = 0;
            float rz = 0;
            rx = Random.value * 6 - 3;
            rz = Random.value * 4 + 2;
            Target.localPosition = new Vector3(rx, 0.5f, rz);

            Target.gameObject.SetActive(true);
        }


        if (other.tag == "goal")
        {
            Point++;
            gRollerAgent ra = other.GetComponent<gRollerAgent>();
            if (null != ra)
            {
                ra.EnteredGoal();
                this.gameObject.SetActive(false);
            }
            float rx = 0;
            float rz = 0;
            rx = Random.value * 4 + 2;
            rz = Random.value * 4 + 2;
            Target.localPosition = new Vector3(rx, 0.5f, rz);

            Target.gameObject.SetActive(true);
        }
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
