using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionDetector : MonoBehaviour
{
    [SerializeField] ThrowableObject throwableObject;

    [SerializeField] BoxCollider2D footCollider;

    [SerializeField] BoxCollider2D parentCollider2D;

    bool isTouchingSpecificObject = false;

    PlayerHaveItem playerHaveItem;

    bool isJump=false;
    void Start()
    {
        playerHaveItem = FindObjectOfType<PlayerHaveItem>();
    }
    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.CompareTag("Ground"))
    //    {
    //        throwableObject.OnStopOrExit();
    //    }
    //}

    void FixedUpdate()
    {
        isJump = isTouchingSpecificObject = false; // フレームの開始時にリセット

        // オブジェクトを持ち上げた際にOnTriggerExitが反応しなくなる為、ここで衝突判定を行っている
        Collider2D[] colliders = Physics2D.OverlapBoxAll(footCollider.bounds.center, footCollider.size, 0);
        foreach (Collider2D col in colliders)
        {
            if ((col.gameObject.tag == "Ground" && parentCollider2D != col))
            {
                throwableObject.OnStopOrExit();
                isTouchingSpecificObject = true;
                break; // 一つでも特定のオブジェクトに触れていれば終了
            }
            if(col.gameObject.CompareTag("JumpPad"))
            {
                isJump=true;
            }
        }

        if (!playerHaveItem.itemOwned) return;
        if(!isTouchingSpecificObject && (playerHaveItem.hasItem.gameObject != parentCollider2D.gameObject)) throwableObject.OnExit();
        //if(isJump)
        //{
        //    throwableObject.OnExit();
        //    throwableObject.VeclocityReset();
        //}
    }
}
