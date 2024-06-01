using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject bgmManagerObject;
    public static GameManager gameManager { get; private set; }

    public static BGMManager bgmManager { get; private set; }

    static FrameRate  frameInstance;

    void Awake()
    {
        #region Singleton
        //インスタンスがnullなら作成
        if (gameManager == null)
        {
            gameManager = this;
            frameInstance =  new FrameRate();
            DontDestroyOnLoad(this);
        }
        //そうではない場合自身を削除
        else { Destroy(this.gameObject); }

        //インスタンスがあるか確認
        if (bgmManager == null)
        {
            //ない場合ゲームオブジェクトを生成
            //生成したゲームオブジェクトについてあるBGMManagerをシングルトンにする
            GameObject bgmObject = Instantiate(bgmManagerObject);
            if (bgmObject.TryGetComponent(out BGMManager bgmSc))
            {
                bgmManager = bgmSc;
            }
            //BGMManagerがない場合生成する
            else
            {
                bgmManager = bgmObject.gameObject.AddComponent<BGMManager>();
            }
            DontDestroyOnLoad (bgmObject);
        }
        else 
        {
            //シングルトンがすでにある場合は
            //シーン繊維後なのでBGM切り替え処理をしておく
            ActiveScene();
            Destroy(this.gameObject); 
        }
        #endregion
    }

    void Start()
    {
        //シーン繊維で音楽の設定を変更
        ActiveScene();
    }
    void ActiveScene()
    {
        //エンディングとタイトルで音楽切り替える
        if (SceneManager.GetActiveScene().name == "result")
        {
            bgmManager.SetEndBGM(BGMSTATE.ENDING);
        }
        else
        {
            bgmManager.SetEndBGM(BGMSTATE.TITLE);
        }
    }
}

public class FrameRate
{
    public FrameRate()
    {
        Application.targetFrameRate = 120;   
    }
}