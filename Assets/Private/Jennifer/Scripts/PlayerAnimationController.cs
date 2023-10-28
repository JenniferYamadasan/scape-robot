//アニメーションの管理をしている


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    /// <summary>
    /// PlayerのAnimatorを格納している
    /// </summary>
    [SerializeField] Animator animator;

    /// <summary>
    /// 現在持っているアイテムを保持しているクラス
    /// </summary>
    [SerializeField] PlayerHaveItem playerHaveItem;

    /// <summary>
    /// 持つアイテムを決めているスクリプト
    /// </summary>
    [SerializeField] ItemCollider itemCollider;

    
    /// アニメーションのパラメータを変数に入れている
    const string WALK_ANIMATION = "isWalk";
    const string ITEM_HAVE_ANIMATION= "hasItem";
    const string DIE_ANIMATION = "isDie";
    const string THROW_ANIMATION = "isThrow";

    BoxCollider2D itemCollider2D;

    /// <summary>
    /// 歩くアニメーションを管理するメソッド
    /// </summary>
    /// <param name="result"></param>
    public void walkAnimator(bool result)
    {
        animator.SetBool(WALK_ANIMATION, result);
    }

    /// <summary>
    /// アイテムを持つアニメーションを流すメソッド
    /// </summary>
    /// <param name="result"></param>
    public void pickUpItem(bool result, BoxCollider2D itemCollider2D)
    {
        this.itemCollider2D = itemCollider2D;
        animator.SetBool(ITEM_HAVE_ANIMATION, result);
    }

    /// <summary>
    /// 持つアイテムが決まった際に呼ばれるメソッド
    /// </summary>
    public void AcquireItem()
    {
        //持つオブジェクトが決まったため、
        //ここで持つオブジェクトを決めているコライダーを無効化する
        itemCollider2D.enabled = false;
        //持つアイテムを取得
        GameObject item = itemCollider.GetNearestObject();
        //アイテムの情報をクラスに引き渡す。
        playerHaveItem.hasItem = item;
        //子オブジェクトに設定する
        playerHaveItem.transform.parent = item.transform;
    }

    /// <summary>
    /// アイテムを投げるアニメーションを流すメソッド
    /// </summary>
    /// <param name="result"></param>
    public void Throw(bool result)
    {
        playerHaveItem.hasItem = null;
        animator.SetBool(THROW_ANIMATION, result);
    }

    /// <summary>
    /// 死んだ瞬間に流すアニメーション
    /// </summary>
    public Animator DieAnimation()
    {
        animator.SetBool(DIE_ANIMATION, true);
        return animator;
    }
}
