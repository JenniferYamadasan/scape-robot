//ボタンのベースクラス

using System;
using UnityEngine;

// 抽象クラスとしてのUIBase。各UIはこのクラスを継承する
public abstract class UIBase : MonoBehaviour
{
    // UIが閉じるアニメーションを完了したときに呼び出されるイベント
    public event Action OnCloseComplete;

    // UIを開くメソッド（アニメーションを再生するかどうかを引数で指定）
    public abstract void Open(bool playAnimation);
    // UIを閉じるメソッド（アニメーションを再生するかどうかを引数で指定）
    public abstract void BackClose(bool playAnimation);

    // 閉じるアニメーションが完了したことを通知するメソッド
    protected void CloseComplete()
    {
        OnCloseComplete?.Invoke();
    }
}
