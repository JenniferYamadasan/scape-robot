using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LimitCheck : MonoBehaviour
{
    //ルーム名前
    [SerializeField] TMP_InputField mInputText;

    //最大文字数
    [SerializeField] int _LENGTH_LIMIT = 3;

    int mLENGTH_LIMIT => _LENGTH_LIMIT;

#region  -------------------　公開処理　------------------------
    /// <summary>
    /// 文字数カウントして特定の文字より大きかったら収めるようにする
    /// </summary>
    public void CheckTextCount()
    {
        if (mInputText.text.Length > mLENGTH_LIMIT)
        {
            mInputText.text = mInputText.text[..mLENGTH_LIMIT];
        }
    }
#endregion ----------------------------------------------------
}
