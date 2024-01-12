using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldCollision : MonoBehaviour
{
    //���g�������G��Ă��邩�ǂ���
    public bool isCollsion { get; private set; } =false;


    //���ɐG�ꂽ��isCollsion��true�ɂ���
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == 8)
        {
            isCollsion = true;
        }
    }

    //���ɗ��ꂽ��isCollsion��false�ɂ���
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == 8)
        {
            isCollsion = false;
        }
    }
}
