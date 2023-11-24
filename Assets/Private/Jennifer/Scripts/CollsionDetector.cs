using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionDetector : MonoBehaviour
{
    [SerializeField] ThrowableObject throwableObject;

   

    void Start()
    {
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
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
