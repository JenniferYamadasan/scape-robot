using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MobileObstacle : MonoBehaviour
{
    enum MOVEVECTOR
    {
        HORIZONTAL = 0,
        VERTICAL,
    }

    [SerializeField] MOVEVECTOR moveVec;

    [SerializeField] Rigidbody2D rb2D;
    [SerializeField,Header("�ړ�����X�s�[�h���L�т邪���̕��ړ����鋗�����L�т�")] float moveFloerSpeed = 5;

    float reverseCount;
    [SerializeField,Header("��������̂ɕK�v�Ȏ���(�傫������ƈړ����鋗�����L�т�)")] float reverseRate=200;

    int reverse = 1;
    Vector2 moveVector;

    PlayerHaveItem playerHaveItem;

    void Start()
    {

        playerHaveItem = FindObjectOfType<PlayerHaveItem>();
        rb2D.bodyType = RigidbodyType2D.Kinematic;
        rb2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        reverseCount = 0;
        switch (moveVec)
        {
            case MOVEVECTOR.HORIZONTAL:
                moveVector = new Vector2(1, 0);
                break;
            case MOVEVECTOR.VERTICAL:
                moveVector = new Vector2(0, 1);
                break;
        }
    }

    void FixedUpdate()
    {
        //�ړ�����
        rb2D.velocity = moveVector * moveFloerSpeed * reverse;

        //���]����
        if (reverseCount % reverseRate == 0)
        {
            reverse = reverse * -1;
        }
        reverseCount++;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) return;
        if (collision.gameObject.tag == "Ground")
        {
            if (!playerHaveItem.itemOwned)
            {
                reverseCount = 0;
                return;
            }

            if(playerHaveItem.hasItem == collision.gameObject) return;

            reverseCount = 0;
        }
    }

}
