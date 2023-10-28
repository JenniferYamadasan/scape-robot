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
    /// 歩くアニメーションのパラメータの名前を
    /// </summary>
    const string WALK_ANIMATION_NAME = "isWalk";

    const string ITEM_HAVE_ANIMATION_NAME = "hasItem";

    const string DIE_ANIMATION = "isDie";

    /// <summary>
    /// 歩くアニメーションを管理するメソッド
    /// </summary>
    /// <param name="result"></param>
    public void walkAnimator(bool result)
    {
        animator.SetBool(WALK_ANIMATION_NAME, result);
    }

    /// <summary>
    /// アイテムを持つか管理するメソッド
    /// </summary>
    /// <param name="result"></param>
    public void pickUpItem(bool result)
    {
        if(!result)
        {
            animator.SetBool(ITEM_HAVE_ANIMATION_NAME, false);
        }
        //AnimationEventでアイテムを拾う処理を行う予定
        else
        {
            animator.SetBool(ITEM_HAVE_ANIMATION_NAME, true);
        }
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
