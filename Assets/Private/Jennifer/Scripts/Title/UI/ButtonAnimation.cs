//ボタンにつけるスクリプト

using UnityEngine;

public class ButtonAnimation : UIBase
{
    [SerializeField] Animator mAnimator = null;

    //UIを戻す処理なのか
    bool mIsBack = false;

    #region  -------------------　公開処理　------------------------
    // UIを開くメソッド（アニメーションを再生するかどうかを引数で指定）
    public override void Open(bool play_animation)
    {
        if (play_animation && mAnimator != null)
        {
            // 開くアニメーションを再生する
            mAnimator.SetTrigger("Open");
        }
        else
        {
            // アニメーションを再生しない場合、UIを直接表示
            gameObject.SetActive(true);
        }
        UIHistory.OpenUI(this);
    }

    /// <summary>
    /// UIを戻す処理
    /// アニメーションを再生するかどうかを引数で指定）
    /// </summary>
    /// <param name="play_animation"></param>
    public override void BackClose(bool play_animation)
    {
        PerformClose(play_animation);
        mIsBack = true;
    }

    /// <summary>
    /// 現在開いているUIを閉じる
    /// </summary>
    /// <param name="play_animation"></param>
    public void CloseCurrentUI(bool play_animation)
    {
        PerformClose(play_animation);
        mIsBack = false;
    }

    /// <summary>
    ///  アニメーターから呼び出されるメソッド
    ///  閉じるアニメーションが完了した時に呼び出される
    ///  前開いてたUIを戻す処理
    ///  単純にUIを閉じる場合何もしない　
    /// </summary>
    public void OnCloseAnimationComplete()
    {
        //UIを戻す処理ではなかったら何もしない
        if (!mIsBack) return;
        //UIベースのクラスを呼ぶ
        CloseComplete();
    }
    #endregion  -----------------------------------------------------


    #region ----------------------　内部処理　-------------------------

    /// <summary>
    /// UIを閉じる処理
    /// </summary>
    /// <param name="play_animation"></param>
    void PerformClose(bool play_animation)
    {
        if (play_animation && mAnimator != null)
        {
            // 閉じるアニメーションを再生する
            mAnimator.SetTrigger("Close");
        }
        else
        {
            // アニメーションを再生しない場合、UIを非表示
            gameObject.SetActive(false);
            // 閉じるアニメーションが完了したことを通知
            CloseComplete();
        }
    }
    #endregion  -----------------------------------------------------

}