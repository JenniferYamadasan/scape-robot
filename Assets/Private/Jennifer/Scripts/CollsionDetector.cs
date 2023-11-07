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
    void Start()
    {
        playerHaveItem = FindObjectOfType<PlayerHaveItem>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            throwableObject.OnStopOrExit();
        }
    }

    void FixedUpdate()
    {
        isTouchingSpecificObject = false; // フレームの開始時にリセット

        Collider2D[] colliders = Physics2D.OverlapBoxAll(footCollider.bounds.center, footCollider.size, 0);
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.tag == "Ground" && parentCollider2D != col)
            {
                Debug.Log($"触れているオブジェクトの名前{col.gameObject.name}");
                throwableObject.OnStopOrExit();
                isTouchingSpecificObject = true;
                break; // 一つでも特定のオブジェクトに触れていれば終了
            }
        }

        if (!playerHaveItem.itemOwned) return;
        if(!isTouchingSpecificObject && (playerHaveItem.hasItem.gameObject != parentCollider2D.gameObject)) throwableObject.OnExit();
    }

    //void OnTriggerExit2D(Collider2D collider2D)
    //{
    //    if (collider2D.gameObject.CompareTag("Ground"))
    //    {
    //        throwableObject.OnStopOrExit(false);
    //    }
    //}
}
