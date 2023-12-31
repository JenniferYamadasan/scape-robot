using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionDetector : MonoBehaviour
{
    [SerializeField] ThrowableObject throwableObject;

  
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground") || collider.gameObject.layer == 8 || collider.gameObject.layer == 6)
        {
            throwableObject.isThrow = false;
            throwableObject.OnStopOrExit(true);
            throwableObject.OnStopOrExit(false);
        }
    }

    void FixedUpdate()
    {

        //if(isJump)
        //{
        //    throwableObject.OnExit();
        //    throwableObject.VeclocityReset();
        //}
    }
}
