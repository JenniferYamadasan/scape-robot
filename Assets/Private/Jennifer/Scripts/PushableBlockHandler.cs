using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlockHandler : MonoBehaviour
{
    [SerializeField] Animator animator;

    [SerializeField, Header("長押しが必要かどうか")] bool isLongPress;

    // アニメーション名の定数
    const string PRESS_ANIMATIONNAME = "Press";

    //当たり判定
    [SerializeField] BoxCollider2D pushSwitchCollider;

    [Header("消失させるオブジェクト"), SerializeField] List<GameObject> disappearingObject = new List<GameObject>();
    [Header("表示させるオブジェクト"), SerializeField] List<GameObject> activeListObjects = new List<GameObject>();

    /// <summary>
    /// トリガーに入った時の処理
    /// </summary>
    /// <param name="collision">衝突したコライダー</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // レイヤーが11または6の場合
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 6)
        {
            if (isLongPress)
            {
                // 長押しが必要な場合
                animator.SetBool(PRESS_ANIMATIONNAME, true);
                ToggleDisplay(disappearingObject, false);
                ToggleDisplay(activeListObjects, true);
                return;
            }

            // 消失オブジェクトと表示オブジェクトが存在しない場合は何もしない
            if (disappearingObject.Count == 0 && activeListObjects.Count == 0) return;

            SetDisappearingObject(true);
        }
    }

    void Update()
    {
        // オブジェクトがトリガーから出たかを判定するための処理
        Collider2D[] colliders = Physics2D.OverlapBoxAll(pushSwitchCollider.bounds.center, pushSwitchCollider.size, 0);
        bool isTouchingGround = false;

        foreach (Collider2D col in colliders)
        {
            if (colliders == null) return;

            if (col.gameObject.layer == 11 || col.gameObject.layer == 6)
            {
                isTouchingGround = true;
                break; // いずれかのオブジェクトが接触している場合はループを抜ける
            }
        }

        // 接触していない場合で長押しが不要な場合はオブジェクトを消す
        if (!isTouchingGround && !isLongPress) SetDisappearingObject(false);
    }

    /// <summary>
    /// オブジェクトの消失状態を設定する
    /// </summary>
    /// <param name="isTouchingGround">地面に接触しているかどうか</param>
    void SetDisappearingObject(bool isTouchingGround)
    {
        animator.SetBool(PRESS_ANIMATIONNAME, isTouchingGround);
        ToggleDisplay(disappearingObject, !isTouchingGround);
        ToggleDisplay(activeListObjects, isTouchingGround);
    }

    /// <summary>
    /// オブジェクトの表示/非表示を切り替える
    /// </summary>
    /// <param name="toggleObjects">切り替えるオブジェクトのリスト</param>
    /// <param name="isActive">表示するかどうか</param>
    void ToggleDisplay(List<GameObject> toggleObjects, bool isActive)
    {
        if (toggleObjects == null) return;

        for (int i = toggleObjects.Count - 1; i >= 0; i--)
        {
            if (toggleObjects[i] == null)
            {
                toggleObjects.RemoveAt(i);
            }
            else
            {
                toggleObjects[i].SetActive(isActive);
            }
        }
    }
}