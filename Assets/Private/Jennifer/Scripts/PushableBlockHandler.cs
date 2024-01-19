using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PushableBlockHandler : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField,Header("���������ǂ���")] bool isLongPress;
    const string PRESS_ANIMATIONNAME = "Press";

    [SerializeField] BoxCollider2D pushSwitchCollider;

    
    [Header("�폜����u���b�N"),SerializeField] List<GameObject> disappearingObject = new List<GameObject>();
    [Header("�\������u���b�N"),SerializeField] List<GameObject> activeListObjects = new List<GameObject> ();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 6)
        {
            if (isLongPress)
            {
                animator.SetBool(PRESS_ANIMATIONNAME, true);
                ToggleDisplay(disappearingObject, false);
                ToggleDisplay(activeListObjects, true);
                return;
            }
            if (disappearingObject.Count == 0 && activeListObjects.Count == 0) return;
            SetDisappearingObject(true);
        }
    }

    void Update()
    {
        // �I�u�W�F�N�g�������グ���ۂ�OnTriggerExit���������Ȃ��Ȃ�ׁA�����ŏՓ˔�����s���Ă���
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pushSwitchCollider.bounds.center, pushSwitchCollider.size, 0);
        bool isTouchingGround = false;
        foreach (Collider2D col in colliders)
        {
            if (colliders == null) return;
            if (col.gameObject.layer == 11 || col.gameObject.layer == 6)
            {
                isTouchingGround = true;
                break; // ��ł�����̃I�u�W�F�N�g�ɐG��Ă���ΏI��
            }
        }

        if (!isTouchingGround && !isLongPress) SetDisappearingObject(false);
    }

    void SetDisappearingObject(bool isTouchingGround)
    {
        animator.SetBool(PRESS_ANIMATIONNAME, isTouchingGround);
        ToggleDisplay(disappearingObject, !isTouchingGround);
        ToggleDisplay(activeListObjects, isTouchingGround);
    }

    void ToggleDisplay(List<GameObject> toggleObjects,bool isActive)
    {
        if (toggleObjects == null) return;

        for (int i = toggleObjects.Count - 1; i >= 0; i--)
        {
            if (toggleObjects[i] == null)
            {
                toggleObjects.Remove(toggleObjects[i].gameObject);
            }
            else
            {
                toggleObjects[i].SetActive(isActive);
            }
        }
    }
}
