using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpPad : MonoBehaviour
{

    /// <summary>
    /// �G�ꂽ�I�u�W�F�N�g���i�[���Ă���ϐ�
    /// </summary>
    List<GameObject> items = new List<GameObject>();

    [SerializeField] float jumpPower;

    [SerializeField] Animator animator;

    const string JUMP_ANIMATION_NAME = "isJump";


    PlayerController playerController;
    void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }

    public void AddPower()
    {
        if (!items.Contains(playerController.gameObject)) return;
        animator.SetBool(JUMP_ANIMATION_NAME, true);
        foreach (GameObject item in items)
        {
            if (item.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D))
            {
                rb2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            }
        }
        ItemReset();

    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.transform.parent == null) return;
        //���X�g�ɃA�C�e�������邩�m�F��������擾
        if (!items.Contains(collider2D.transform.parent.gameObject))
        {
            items.Add(collider2D.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.transform.parent == null) return;
        //���X�g�ɃA�C�e�������邩�m�F��������폜
        if (items.Contains(collider2D.transform.parent.gameObject))
        {
            items.Remove(collider2D.transform.parent.gameObject);
            animator.SetBool(JUMP_ANIMATION_NAME, false);
        }
    }

    /// <summary>
    /// ���X�g�̒��g���폜
    /// </summary>
    public void ItemReset()
    {
        if (items.Count == 0) return;
        items.Clear();
    }

}
