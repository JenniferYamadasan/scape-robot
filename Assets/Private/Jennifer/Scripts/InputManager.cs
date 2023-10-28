//プレイヤーの入力値を管理している


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 現在行えるアクション　アイテムを持つかアイテムを投げるか
/// </summary>
public enum ITEMACTION
{
    HOLD = 0,
    THROW,
}
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

    /// <summary>
    /// 自殺するまでに必要な時間
    /// </summary>
    [SerializeField] float dieTime;

    /// <summary>
    /// プレイヤーの挙動を管理するスクリプト
    /// </summary>
    [SerializeField] PlayerController playerController;

    /// <summary>
    /// 現在アイテムを持っている状態か投げている状態かのステータス
    /// </summary>
    public ITEMACTION itemAction { get; private set; } = ITEMACTION.HOLD;


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
        if (context.started) isJump = true;
        if (context.canceled) isJump = false;
    }

    /// <summary>
    /// ジャンプの入力値をfalseにするメソッド
    /// </summary>
    public void IsJumpFinish()
    {
        isJump = false;
    }
    /// <summary>
    /// 移動の入力値をfalseにするメソッド
    /// </summary>
    public void IsMoveFinish()
    {
        inputVec = Vector2.zero;
        playerController.OnDie();
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

    /// <summary>
    /// アイテムを持つボタンを押された際に呼ばれるメソッド
    /// </summary>
    /// <param name="context"></param>
    public void InputHold_Throw(InputAction.CallbackContext context)
    {
        //ボタンが押された瞬間にアイテムを持つ又は投げる
        if(context.started)
        {
            playerController.IsItemHeld(itemAction);
        }
    }

    /// <summary>
    /// ステートの更新
    /// </summary>
    /// <param name="itemAction"></param>
    public void ChangeState(ITEMACTION itemAction)
    {
        this.itemAction = itemAction;
    }
}
