using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PushableBlockHandler : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField,Header("長押しかどうか")] bool isLongPress;
    const string PRESS_ANIMATIONNAME = "Press";

    [SerializeField] BoxCollider2D pushSwitchCollider;

    [SerializeField] List<GameObject> disappearingObject = new List<GameObject>();
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (isLongPress) return;

    //    //触れたオブジェクトがアイテム　またはPlayerだったら
    //    if(collision.gameObject.layer ==6 || collision.gameObject.tag == "Player")
    //    {
    //        animator.SetBool(PRESS_ANIMATIONNAME, true);
    //    }
    //}

    void Update()
    {
        // オブジェクトを持ち上げた際にOnTriggerExitが反応しなくなる為、ここで衝突判定を行っている
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pushSwitchCollider.bounds.center, pushSwitchCollider.size, 0);
        bool isTouchingGround = false;
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.layer == 11 || col.gameObject.layer == 6)
            {
                isTouchingGround = true;
                break; // 一つでも特定のオブジェクトに触れていれば終了
            }
        }

        animator.SetBool(PRESS_ANIMATIONNAME, isTouchingGround);
        foreach (var item in disappearingObject)
        {
            item.SetActive(!isTouchingGround);
        }
    }

}
