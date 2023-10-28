//プレイヤーの挙動を制御している

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// 入力値を格納するクラスのインスタンスを格納する
    /// </summary>
    [SerializeField] InputManager inputManager;

    /// <summary>
    /// RigidBodyを格納する変数
    /// </summary>
    [field: SerializeField] public Rigidbody2D rb2D { get; private set; }

    /// <summary>
    /// 移動スピードを調整する変数
    /// </summary>
    [SerializeField] float moveSpeed;


    /// <summary>
    /// ジャンプ力
    /// </summary>
    [SerializeField] float jumpPower;

    /// <summary>
    /// 地面にいるかどうか
    /// </summary>
    bool isGround = true;

    /// <summary>
    /// アイテムを持っているかどうか
    /// </summary>
    bool hasItem = false;


    /// <summary>
    /// プレイヤーのモデルを格納している
    /// </summary>
    [SerializeField] GameObject playerModel;

    /// <summary>
    /// プレイヤーのアニメーションを管理するクラス
    /// </summary>
    [SerializeField] PlayerAnimationController playerAnimationController;

    /// <summary>
    /// このゲームオブジェクトに触れているアイテムを拾う
    /// </summary>
    [SerializeField] BoxCollider2D itemCollider2D;

    /// <summary>
    /// 死んでいるかどうか
    /// </summary>
    bool isDie = false;


    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        OnMove();  //キャラクターを移動させる
        if (!isGround) return;
        OnJump();//ジャンプする処理
    }

    /// <summary>
    /// プレイヤーが移動するメソッド
    /// </summary>
    [System.Obsolete]
    void OnMove()
    {
        //入力値を変数に格納している
        Vector2 inputVec = inputManager.inputVec;
        OnRotate(inputVec);
        //ここで実際に移動する Y座標を0にするとジャンプができないようになるため、別途現在のY座標を足している
        rb2D.velocity = new Vector2(inputVec.x, 0) * moveSpeed + new Vector2(0, rb2D.velocity.y);

        //キャラクターが動いているかどうか調べてアニメーションを設定する
        if (Mathf.Abs(rb2D.velocity.x) > 0)
        {
            playerAnimationController.walkAnimator(true);
        }
        else if(Mathf.Abs(rb2D.velocity.x) <=0)
        {
            playerAnimationController.walkAnimator(false);
        }
    }

    /// <summary>
    /// キャラクターの回転をするメソッド
    /// </summary>
    /// <param name="inputVec"></param>
    void OnRotate(Vector2 inputVec)
    {
        //入力値に応じてキャラクターの向く方向を変更している
        if (inputVec.x > 0)
        {
            playerModel.transform.rotation = Quaternion.Euler(0, 140, 0);
        }
        if (inputVec.x < 0)
        {
            playerModel.transform.rotation = Quaternion.Euler(0, -140, 0);
        }
    }
    /// <summary>
    /// ジャンプをするメソッド
    /// </summary>
    void OnJump()
    {
        //ジャンプボタンが押されていて、地面に足がついている
        if (inputManager.isJump && isGround)
        {
            rb2D.AddForce(transform.up * jumpPower);
            isGround = false;
            inputManager.IsJumpFinish();
        }
    }

    /// <summary>
    /// アイテムを持つメソッドアイテムをすでに持っていたら何もしないようにする。
    /// </summary>
    public void IsItemHeld(ITEMACTION itemAction)
    {
        switch (itemAction)
        {
            case ITEMACTION.HOLD:
                if (hasItem) return;
                itemCollider2D.enabled = true;
                playerAnimationController.pickUpItem(true,itemCollider2D);
                break;
            case ITEMACTION.THROW:
                playerAnimationController.pickUpItem(false, null);
                playerAnimationController.Throw(true);
                break;
        }
    }

    /// <summary>
    /// 死んだ際に呼ぶメソッドここで
    /// </summary>
    public void OnDie()
    {
        if (isDie) return;
        Debug.Log("死ぬ");
        isDie = true;
    }

    /// <summary>
    /// 地面にいるかどうかの情報を取得するメソッド
    /// </summary>
    /// <param name="result"></param>
    public void IsGround(bool result)
    {
        isGround = result;
    }
}
