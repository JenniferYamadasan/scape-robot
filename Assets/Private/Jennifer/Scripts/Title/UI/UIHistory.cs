using System.Collections.Generic;

public static class UIHistory
{
    // UI履歴を保持するスタック
    private static Stack<UIBase> mUiHistory = new Stack<UIBase>();

    // 現在表示されているUI
    private static UIBase mCurrentUI;

    // 戻る時に開くアニメーションを再生するかどうかのフラグ
    private static bool mPlayOpenAnimationOnBack = true;

    // 現在のUIを閉じる時に閉じるアニメーションを再生するかどうかのフラグ
    private static bool mPlayCloseAnimationOnCurrent = true;


    #region  ---------------　公開処理　---------------
    // 新しいUIを開くメソッド
    public static void OpenUI(UIBase newUI)
    {
        // 現在のUIが存在する場合
        if (mCurrentUI != null)
        {
            // 現在のUIを履歴に追加
            mUiHistory.Push(mCurrentUI);

            // 現在のUIを閉じる（アニメーションを再生する）
            mCurrentUI.BackClose(true);
        }

        // 新しいUIに設定する
        mCurrentUI = newUI;

        // 新しいUIを開く（アニメーションを再生する）
        mCurrentUI.Open(true);
    }

    /// <summary>
    /// 戻るボタンが押された時の動作を制御するメソッド
    /// </summary>
    /// <param name="playCloseAnimation">現在のUIを閉じる時にアニメーションを再生するかどうか</param>
    /// <param name="playOpenAnimation">前のUIを開く時にアニメーションを再生するかどうか</param>
    public static void Back
    (
        bool playCloseAnimation = true,
        bool playOpenAnimation = true
    )
    {
        // 閉じるアニメーションのフラグを設定
        mPlayCloseAnimationOnCurrent = playCloseAnimation;

        // 開くアニメーションのフラグを設定
        mPlayOpenAnimationOnBack = playOpenAnimation;

        // 現在のUIが存在する場合
        if (mCurrentUI != null)
        {
            // 閉じるアニメーションを再生する場合
            if (mPlayCloseAnimationOnCurrent)
            {
                // 現在のUIを閉じる（アニメーションを再生する）
                mCurrentUI.BackClose(true);

                // 閉じるアニメーション完了時のイベントハンドラを登録
                mCurrentUI.OnCloseComplete += HandleCloseComplete;
            }
            else
            {
                // アニメーションを再生しない場合、直接閉じる処理を呼び出す
                HandleCloseComplete();
            }
        }
    }

    /// <summary>
    /// シーン遷移した際に履歴を消去する
    /// </summary>
    public static void ResetHistory()
    {
        mUiHistory.Clear();
        mCurrentUI = null;
        mPlayOpenAnimationOnBack = false;
        mPlayCloseAnimationOnCurrent = false;
    }

    #endregion-----------------------------------------------------



    #region  ----------------　内部処理　----------------------------
    /// <summary>
    /// 閉じるアニメーションが完了した時に呼び出されるメソッド
    /// UI履歴があると前開いてたUIを表示する
    /// </summary>
    private static void HandleCloseComplete()
    {
        // 閉じるアニメーションの完了時のイベントハンドラを解除
        if (mPlayCloseAnimationOnCurrent)
        {
            mCurrentUI.OnCloseComplete -= HandleCloseComplete;
        }

        // UI履歴がある場合
        if (mUiHistory.Count > 0)
        {
            // 一つ前のUIをポップして取得
            mCurrentUI = mUiHistory.Pop();

            // 開くアニメーションを再生する場合
            if (mPlayOpenAnimationOnBack)
            {
                // 前のUIを開く（アニメーションを再生する）
                mCurrentUI.Open(true);
            }
            else
            {
                // アニメーションを再生しない場合、UIを直接表示
                mCurrentUI.gameObject.SetActive(true);
            }
        }
        else
        {
            // 履歴がない場合、現在のUIをnullにする
            mCurrentUI = null;
        }
    }

    #endregion --------------------------------------------------------
}
