using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVigilAction
{
    public void GetResultFromBot(Transform transform, MovingBot bot);
    public void DeleteTransform(Transform transform);
}
