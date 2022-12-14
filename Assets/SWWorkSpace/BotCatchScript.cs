using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCatchScript : MonoBehaviour
{
    MovingBot _parent;
    private void OnEnable()
    {
        _parent = GetComponentInParent<MovingBot>();

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Target") 
        {
            Debug.Log("catch" + other.gameObject.name);
            _parent.CatchAlert(other.transform);
            other.enabled = false;
        }

    }

}
