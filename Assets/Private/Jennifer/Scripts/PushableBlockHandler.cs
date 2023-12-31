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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isLongPress) return;

        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 6)
        {
            SetDisappearingObject(true);
        }
    }

    void Update()
    {
       

        // オブジェクトを持ち上げた際にOnTriggerExitが反応しなくなる為、ここで衝突判定を行っている
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pushSwitchCollider.bounds.center, pushSwitchCollider.size, 0);
        bool isTouchingGround = false;
        foreach (Collider2D col in colliders)
        {
            if (colliders == null) return;
            if (col.gameObject.layer == 11 || col.gameObject.layer == 6)
            {
                isTouchingGround = true;
                break; // 一つでも特定のオブジェクトに触れていれば終了
            }
        }

        if (!isTouchingGround) SetDisappearingObject(false);
    }

    void SetDisappearingObject(bool isTouchingGround)
    {
        animator.SetBool(PRESS_ANIMATIONNAME, isTouchingGround);
        if (disappearingObject == null) return;

        for (int i = disappearingObject.Count-1; i >=0; i--)
        {
            if (disappearingObject[i] == null)
            {
                disappearingObject.Remove(disappearingObject[i].gameObject);
            }
            else
            {
                disappearingObject[i].SetActive(!isTouchingGround);
            }
        }
    }
}
