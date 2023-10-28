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
    [SerializeField] Rigidbody2D rb2D;

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

    [SerializeField,Tooltip("最大の重力量")] float maxGravity;
    [SerializeField, Tooltip("加える重力の量")] float upGravity;


    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        OnMove();  //キャラクターを移動させる
        isItemHeld();//アイテムを持つ処理
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
        //ここで実際に移動する
        rb2D.velocity = new Vector2(inputVec.x, 0) * moveSpeed + new Vector2(0, rb2D.velocity.y);
        playerAnimationController.walkAnimator(true);
    }

    /// <summary>
    /// キャラクターの回転をするメソッド
    /// </summary>
    /// <param name="inputVec"></param>
    void OnRotate(Vector2 inputVec)
    {
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
        }
    }

    void isItemHeld()
    {
        if (hasItem) return;
        playerAnimationController.pickUpItem(true);
    }

    public void OnDie()
    {
        Debug.Log("死ぬ");
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
