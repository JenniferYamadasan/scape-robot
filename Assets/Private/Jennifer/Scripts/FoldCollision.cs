using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldCollision : MonoBehaviour
{
    //自身が床部触れているかどうか
    public bool isCollsion { get; private set; } =false;

    [SerializeField] BoxCollider2D collider2D;
    //床に触れたらisCollsionをtrueにする
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == 8)
        {
            isCollsion = true;
        }
    }

    //床に離れたらisCollsionをfalseにする
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == 8)
        {
            isCollsion = false;
        }
    }

    void Update()
    {
        // オブジェクトを持ち上げた際にOnTriggerExitが反応しなくなる為、ここで衝突判定を行っている
        Collider2D[] colliders = Physics2D.OverlapBoxAll(collider2D.bounds.center, collider2D.size, 0);
        //現在床に触れているかどうか
        bool isTouchingGround = false;
        foreach (Collider2D col in colliders)
        {
            //触れたオブジェクトによってオブジェクトのベクトルを初期化したり、物理挙動を無効化したりしている
            if (col.gameObject.CompareTag("Ground") && col.gameObject.layer == 8)
            {
                isTouchingGround = true;
            }
        }

        isCollsion = isTouchingGround;
    }
}
