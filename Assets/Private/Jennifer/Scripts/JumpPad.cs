using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class JumpPad : MonoBehaviour
{

    /// <summary>
    /// 触れたオブジェクトを格納している変数
    /// </summary>
    List<GameObject> items = new List<GameObject>();

    [SerializeField] float jumpPower;

    [SerializeField] Animator animator;

    const string JUMP_ANIMATION_NAME = "isJump";

    [SerializeField] float coolTime;
    PlayerController playerController;

    bool isJump =false;
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
                StartCoroutine(enumerator(rb2D));
            }
        }
        ItemReset();
    }
    IEnumerator enumerator(Rigidbody2D rb2D)
    {        
        animator.SetBool(JUMP_ANIMATION_NAME, true);
        yield return new WaitForSeconds(coolTime);

        if (!isJump) yield break;
        rb2D.velocity = Vector2.up * jumpPower + new Vector2(rb2D.velocity.x, 0);

        yield return new WaitForSeconds(0.2f);
        animator.SetBool(JUMP_ANIMATION_NAME, false);
    }
    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.transform.parent == null) return;
        //リストにアイテムがあるか確認あったら取得
        if (!items.Contains(collider2D.transform.parent.gameObject))
        {
            items.Add(collider2D.transform.parent.gameObject);
            AddPower();
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.transform.parent == null) return;
        //リストにアイテムがあるか確認あったら削除
        if (items.Contains(collider2D.transform.parent.gameObject))
        {
            items.Remove(collider2D.transform.parent.gameObject);
            animator.SetBool(JUMP_ANIMATION_NAME, false);
        }
    }

    public void JumpCancel()
    {
        isJump = false;
    }
    /// <summary>
    /// リストの中身を削除
    /// </summary>
    public void ItemReset()
    {
        isJump = true;
        if (items.Count == 0) return;
        items.Clear();
    }

}
