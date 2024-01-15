using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldCollision : MonoBehaviour
{
    //���g�������G��Ă��邩�ǂ���
    public bool isCollsion { get; private set; } =false;

    [SerializeField] BoxCollider2D collider2D;
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

    void Update()
    {
        // �I�u�W�F�N�g�������グ���ۂ�OnTriggerExit���������Ȃ��Ȃ�ׁA�����ŏՓ˔�����s���Ă���
        Collider2D[] colliders = Physics2D.OverlapBoxAll(collider2D.bounds.center, collider2D.size, 0);
        //���ݏ��ɐG��Ă��邩�ǂ���
        bool isTouchingGround = false;
        foreach (Collider2D col in colliders)
        {
            //�G�ꂽ�I�u�W�F�N�g�ɂ���ăI�u�W�F�N�g�̃x�N�g����������������A���������𖳌��������肵�Ă���
            if (col.gameObject.CompareTag("Ground") && col.gameObject.layer == 8)
            {
                isTouchingGround = true;
            }
        }

        isCollsion = isTouchingGround;
    }
}
