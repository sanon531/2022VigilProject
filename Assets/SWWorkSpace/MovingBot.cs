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
    public EBot thisbot ;
    // Start is called before the first frame update
    IVigilAction mastermind;
    IAstarAI ai;
    Transform _targetTransform;

    Vector3 originalpos;
    void OnEnable()
    {
        originalpos = transform.position;
        ai = GetComponent<IAstarAI>();
        ai.onSearchPath += MoveToTargetAstar;
        mastermind = GetComponentInParent<IVigilAction>();
    }
    void OnDisable()
    {
        SetMovableFalse();
    }
    [SerializeField]
    Vector3 target;
    public void MoveToTarget(Transform val) 
    {
        _targetTransform = val;
        target = _targetTransform.position;
        MoveToTargetAstar();
    }
    public void MovetoOrigin() 
    {
        _targetTransform = null;
        target = originalpos;
        MoveToTargetAstar();
    }
    void MoveToTargetAstar() 
    {
        if (target != null && ai != null) 
            ai.destination = target;
    }
    public void SetMovableFalse() 
    {
        ai.canMove = false;
    }

    public void CatchAlert(Transform transform) 
    {
        mastermind.GetResultFromBot(_targetTransform, this);
        mastermind.DeleteTransform(transform);
    }

}
