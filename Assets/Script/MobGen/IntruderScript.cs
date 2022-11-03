using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntruderScript : MonoBehaviour
{

    [SerializeField]
    Rigidbody2D _rigidbody2D;
    [SerializeField]
    Collider2D _collider2D;
    [SerializeField]
    bool _isActivated = false;
    [SerializeField]
    IntruderManager _parentManager;


    public void SetActivate() 
    {
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "outerZone" || collision.transform.tag == "vigil")
        {
            //_parentManager.CallFail();
        } else if (collision.transform.tag == "flagZone") 
        {
            //_parentManager.Callsuccess();

        }
    }
}
