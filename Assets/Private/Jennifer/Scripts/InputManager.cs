//プレイヤーの入力値を管理している


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    /// <summary>
    /// 移動の入力値を格納する変数(入力した時点でノーマライズはされている。)
    /// </summary>
    public Vector2 inputVec { get; private set; }

    /// <summary>
    /// ジャンプボタンを押したかどうか
    /// </summary>
    public bool isJump { get; private set; }

    /// <summary>
    /// ブロックを持つを押したかどうか
    /// </summary>
    public bool isHold { get; private set; }

    /// <summary>
    /// 自爆ボタンを押した時間
    /// </summary>
    float dieTimer = 0.0f;

    [SerializeField] float dieTime;

    [SerializeField] PlayerController playerController;


    /// <summary>
    /// 移動ボタンが押されたら入力値を変数に格納する。
    /// </summary>
    /// <param name="context"></param>
    public void InputMoveVec(InputAction.CallbackContext context)
    {
        //入力値を取得
        inputVec = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// ジャンプボタンが押されたら入力値を変数に格納する。
    /// </summary>
    /// <param name="context"></param>
    public void InputJump(InputAction.CallbackContext context)
    {
        //ボタンが押されたらtrue離されたらfalseが返る
        isJump = context.ReadValueAsButton();
    }

   void Update()
    {
        DieTimeCount();//自爆ボタンを押している秒数をカウントする
    }

    /// <summary>
    /// 自爆ボタンを押している秒数を数えるメソッド
    /// </summary>
    void DieTimeCount()
    {
        //自爆ボタンを押していなかったら何もしない
        if (inputVec.y >= 0) return;

        //押していたら数える
        dieTimer += Time.deltaTime;
        if (dieTimer >= dieTime)
        {
            playerController.OnDie();//死んだことをプレイヤーコントローラーに伝える
        }
    }
    public void InputPut(InputAction.CallbackContext context)
    {
        //ボタンが押されたらtrue離されたらfalseが返る
        isHold = context.ReadValueAsButton();
    }
}
