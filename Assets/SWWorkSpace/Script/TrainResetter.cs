using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainResetter : MonoBehaviour
{
    [SerializeField]
    static TrainResetter instance = null;

    [SerializeField]
    List<Transform> transformsOfAgents = new  List<Transform>();

    Dictionary<Transform, Vector3> originalPosDic = new Dictionary<Transform, Vector3>();

    void Start()
    {
        instance = this;
        foreach (var v in transformsOfAgents) 
        {
            originalPosDic.Add(v, v.position);
        }
    }


    public static void ResetPlay() 
    {
        foreach (var v in  instance.transformsOfAgents)
        {
            v.position = instance.originalPosDic[v];
        }
    }

}
