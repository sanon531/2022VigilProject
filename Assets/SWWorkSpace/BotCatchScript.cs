using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCatchScript : MonoBehaviour
{
    [SerializeField]
    EBot targetBot= EBot.Vigil;
    MovingBot _parent;
    MovingBot _current;
    private void OnEnable()
    {
        _parent = GetComponentInParent<MovingBot>();

    }

    private void OnTriggerEnter(Collider other)
    {
        _current = other.GetComponent<MovingBot>();
        if (_current.thisbot == targetBot) 
        {
            Debug.Log("catch" + other.gameObject.name);
            _parent.CatchAlert(other.transform);
            _current.SetMovableFalse();
            other.enabled = false;
        }

    }

}
