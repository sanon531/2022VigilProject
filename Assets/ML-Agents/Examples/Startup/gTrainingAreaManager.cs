using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gTrainingAreaManager : MonoBehaviour
{
    public gTargetManager targetManager = null;
    

    void Start()
    {
        EpisodeStart();
    }

    public void EpisodeStart()
    {
        targetManager.OnEpisodeBegin();
    }

    public void needEpisodeEnd()
    {
        

        EpisodeStart();
    }
}
