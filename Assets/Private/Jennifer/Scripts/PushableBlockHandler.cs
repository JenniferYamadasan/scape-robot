using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PushableBlockHandler : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField,Header("���������ǂ���")] bool isLongPress;
    const string PRESS_ANIMATIONNAME = "Press";

    [SerializeField] BoxCollider2D pushSwitchCollider;

    [SerializeField] List<GameObject> disappearingObject = new List<GameObject>();
    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (isLongPress) return;

    //    //�G�ꂽ�I�u�W�F�N�g���A�C�e���@�܂���Player��������
    //    if(collision.gameObject.layer ==6 || collision.gameObject.tag == "Player")
    //    {
    //        animator.SetBool(PRESS_ANIMATIONNAME, true);
    //    }
    //}

    void Update()
    {
        // �I�u�W�F�N�g�������グ���ۂ�OnTriggerExit���������Ȃ��Ȃ�ׁA�����ŏՓ˔�����s���Ă���
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pushSwitchCollider.bounds.center, pushSwitchCollider.size, 0);
        bool isTouchingGround = false;
        foreach (Collider2D col in colliders)
        {
            if (col.gameObject.layer == 11 || col.gameObject.layer == 6)
            {
                isTouchingGround = true;
                break; // ��ł�����̃I�u�W�F�N�g�ɐG��Ă���ΏI��
            }
        }

        animator.SetBool(PRESS_ANIMATIONNAME, isTouchingGround);
        foreach (var item in disappearingObject)
        {
            item.SetActive(!isTouchingGround);
        }
    }

}
