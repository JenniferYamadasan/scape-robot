//ルーム選択

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomType : MonoBehaviour
{
    public enum Type
    {
        SOLO = 0,
        MULITI,
        FRIEND
    }

    public Type SelectType { get; private set; } = Type.SOLO;

    GameObject mMyObject = null;

    [SerializeField] private ButtonAnimation mButtonAnimation = null;

    #region  ------------------　公開処理 -----------------------
    /// <summary>
    /// ソロを選択した時呼ぶ
    /// </summary>
    public void OnSolo()
    {
        Select(Type.SOLO);
        mButtonAnimation.CloseCurrentUI(mButtonAnimation);
    }

    /// <summary>
    /// マルチを選択した際に呼ぶ
    /// </summary>
    public void OnMuliti()
    {
        Select(Type.MULITI);
        mButtonAnimation.CloseCurrentUI(mButtonAnimation);
    }

    /// <summary>
    /// フレンドを選択したら呼ぶ
    /// </summary>
    public void OnFriend()
    {
        Select(Type.FRIEND);
        mButtonAnimation.CloseCurrentUI(mButtonAnimation);
    }
    #endregion ---------------------------------------------

    #region ------------------　内部処理　--------------------
    private void Start()
    {
        //キャッシュ
        Cache();
    }

    /// <summary>
    /// ゲットコンポーネントなどの作業を行う
    /// </summary>
    private void Cache()
    {
        mMyObject = this.gameObject;
    }

    private void Select(Type type)
    {
        mMyObject.SetActive(false);
        SelectType = type;
    }
    #endregion ---------------------------------------------
}
