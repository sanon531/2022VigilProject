using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public enum EBot
{
    Vigil,
    Intruder,
    Civilian,
}

public class MovingBot : MonoBehaviour
{
    [SerializeField]
    EBot thisbot ;
    // Start is called before the first frame update
    IVigilLearn mastermind;
    IAstarAI ai;

    void OnEnable()
    {
        ai = GetComponent<IAstarAI>();
        ai.onSearchPath += MoveToTargetAstar;
        mastermind = GetComponentInParent<IVigilLearn>();
    }
    void OnDisable()
    {
        if (ai != null) ai.onSearchPath -= MoveToTargetAstar;
    }
    [SerializeField]
    Vector3 target;
    public void MoveToTarget(Vector3 val) 
    {
        target = val;
        MoveToTargetAstar();
    }
    void MoveToTargetAstar() 
    {
        if (target != null && ai != null) 
            ai.destination = target;
    }
    private void OnCollisionEnter(Collision collision)
    {
        mastermind.GetResultFromBot();
    }



}
