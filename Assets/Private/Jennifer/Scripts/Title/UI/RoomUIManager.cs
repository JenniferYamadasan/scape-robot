//ルーム選択時のUIを管理するクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUIManager : MonoBehaviour
{
    // シングルトンインスタンス
    public static RoomUIManager Instance { get; private set; } = null;

    //ルームのタイプ
    [SerializeField] GameObject mRoomtype =null;

    #region  -----------公開処理---------------

    // シーン変更時に呼び出されるメソッド
    public void OnSceneChange()
    {
        // シングルトンインスタンスをクリア
        Instance = null;
        // 不要なアセットをアンロードするためのコルーチンを開始
        StartCoroutine(UnloadUnusedAssets());
    }

    #endregion  --------------------------------


#region ------------- 内部処理 -----------------
    void Awake()
    {
        // シングルトンインスタンスを初期化
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {

    }

    // 不要なアセットをアンロードするコルーチン
    private IEnumerator UnloadUnusedAssets()
    {
        // 使われていないアセットをアンロードし、その処理が完了するまで待機
        yield return Resources.UnloadUnusedAssets();
        // ガベージコレクタを強制的に実行して、使われていないメモリを解放
        System.GC.Collect();
    }

    void OnDestroy()
    {
        // オブジェクトが破棄されるときのリソース解放処理（必要に応じて記述）
    }
    #endregion ------------------------------------
}
