using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionDetector : MonoBehaviour
{
    [SerializeField] ThrowableObject throwableObject;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            throwableObject.OnStop();
        }
    }
}
