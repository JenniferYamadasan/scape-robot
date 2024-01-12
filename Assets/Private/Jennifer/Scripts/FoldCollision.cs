using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldCollision : MonoBehaviour
{
    //自身が床部触れているかどうか
    public bool isCollsion { get; private set; } =false;


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
}
