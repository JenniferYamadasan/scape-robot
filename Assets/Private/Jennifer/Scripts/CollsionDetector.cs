using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionDetector : MonoBehaviour
{
    //親オブジェクト　物理挙動を管理してる
    [SerializeField] ThrowableObject throwableObject;

  
    void OnTriggerEnter2D(Collider2D collider)
    {
        //地面に触れたかどうか
        if (collider.gameObject.CompareTag("Ground") || collider.gameObject.layer == 8 || collider.gameObject.layer == 6)
        {
            //触れたら投げてるフラグを折って物理挙動の制限をかける
            throwableObject.isThrow = false;
            throwableObject.OnStopOrExit();
        }
    }
}
