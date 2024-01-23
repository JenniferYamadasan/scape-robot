//プレイヤーの挙動を制御している

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// キャラクターが向いている向き
/// </summary>
public enum DIRECTION
{
    RIGHT=0,
    LEFT,
}
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

    DIRECTION direction = DIRECTION.RIGHT;

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
    [SerializeField] GameObject itemColliderObj;

    /// <summary>
    /// 上下にオブジェクトが挟まれているか
    /// </summary>
    bool verticalFold => foldManager.upCollsion.isCollsion && isGround;

    /// <summary>
    /// 左右にオブジェクトが挟まれているか
    /// </summary>
    bool horizontalFold => foldManager.rightCollsion.isCollsion && foldManager.leftCollsion.isCollsion;

    
    /// <summary>
    /// 挟まれているかどうか検知するコライダーをclass管理している
    /// </summary>
    [System.Serializable]
    public class FoldCollisionManager
    {
        [field:SerializeField]public FoldCollision upCollsion { get; private set; }
        [field: SerializeField] public FoldCollision leftCollsion { get; private set; }
        [field: SerializeField] public FoldCollision rightCollsion { get; private set; }
    }

    /// <summary> 現在触れている移動する床 </summary>
    GameObject foldObj;
    /// <summary>
    /// 死んでいるかどうか
    /// </summary>
    public bool isDie = false;

    /// <summary> ジャンプしたかどうか </summary>
    public bool isJump  => inputManager.isJump && isGround;

    /// <summary> 動いている床の移動ベクトル </summary>
    Vector2 vector;

    /// <summary> 移動する床に触れているかどうか </summary>
    bool isMoveGround;

    /// <summary> 足元の当たり判定 </summary>
    [SerializeField] FootCollsion footCollsion;

    /// <summary> 挟まれているかどうか検知するコライダーを管理しているクラス </summary>
    [SerializeField] FoldCollisionManager foldManager;


    [System.Obsolete]
    void Update()
    {
        CheackFold();//挟まれているかどうか確認する
        OnMove();  //キャラクターを移動させる
        if (!isGround) return;//地面に触れていない場合この先の挙動をしない
        OnJump();//ジャンプする処理
    }
    
    void CheackFold()
    {
        if (playerAnimationController.isFold) return;
        //移動できる床に触れている際、移動している床のベクトル方向に対してPlayerが挟まっていた場合潰れるアニメーションを流す。
        if (isMoveGround)
        {
            if (verticalFold && Mathf.Abs(vector.y) > 0)
            {
                Debug.Log($"Player = 縦潰れた 頭{foldManager.upCollsion.collsionGameObject}");
                playerAnimationController.IsFold(0);
            }
        }

        //移動している床のベクトル方向に対してPlayerが挟まっていた場合潰れるアニメーションを流す。
        if (horizontalFold && Mathf.Abs(vector.x) > 0)
        {
            Debug.Log("Player = 横潰れた");
            playerAnimationController.IsFold(1);
        }
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
        rb2D.velocity = new Vector2(inputVec.x * moveSpeed + vector.x, 0) + new Vector2(0, rb2D.velocity.y);

        if (!isMoveGround)
        {
            if (footCollsion.isHalf)
            {
                if (inputVec.y < 0)
                {
                    footCollsion.HalfTrigger();
                }
            }
        }


        //キャラクターが動いているかどうか調べてアニメーションを設定する
        if (Mathf.Abs(rb2D.velocity.x) > 0 && Mathf.Abs(inputManager.inputVec.x) >0)
        {
            playerAnimationController.walkAnimator(true);
        }
        else
        {
            playerAnimationController.walkAnimator(false);
        }
    }

    /// <summary>
    /// 移動ベクトルを初期化する
    /// </summary>
    public void OnMoveStop()
    {
        rb2D.velocity = Vector2.zero;
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
            itemColliderObj.transform.rotation = Quaternion.Euler(0, 0, 0);
            direction = DIRECTION.RIGHT;
        }
        if (inputVec.x < 0)
        {
            playerModel.transform.rotation = Quaternion.Euler(0, -140, 0);
            itemColliderObj.transform.rotation = Quaternion.Euler(0, -145, 0);
            direction = DIRECTION.LEFT;
        }

        playerAnimationController.OnRotate(direction);
    }
    /// <summary>
    /// ジャンプをするメソッド
    /// </summary>
    void OnJump()
    {
        //ジャンプボタンが押されていて、地面に足がついている
        if (inputManager.isJump)
        {
            //rb2D.AddForce(transform.up * jumpPower);
            rb2D.velocity = transform.up * jumpPower + new Vector3(rb2D.velocity.x, vector.y, 0);
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
            //行えるアクションがアイテムを持つ状態だったら
            case ITEMACTION.HOLD:
                //アイテムをすでに持っていたら何もしない
                if (hasItem) return;
                //アイテムを持つメソッド呼ぶ
                playerAnimationController.pickUpItem(direction);
                break;
            //行えるアクションがアイテムを投げる状態だったら
            case ITEMACTION.THROW:
                //アイテムを投げるメソッド呼ぶ
                playerAnimationController.Throw(direction);
                break;
        }
    }

    /// <summary>
    /// 自爆の際はnullそれ以外の場合復活はtrue死んだ際はfalse
    /// </summary>
    public void ReviveOrSelfDestruct(bool? result)
    {
        if(result ==null)
        {

        }
        else
        {
            rb2D.velocity = Vector2.zero;
            isDie = (bool)result;
        }
    }
    /// <summary>
    /// 地面にいるかどうかの情報を取得するメソッド
    /// </summary>
    /// <param name="result"></param>
    public void IsGround(bool result)
    {
        isGround = result;
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {

        //ゴールに触れた場合ゴールの当たり判定削除してゴール用のアニメーションを再生する。
        if(collider2D.gameObject.tag=="Goal")
        {
            if (isDie) return;
            collider2D.GetComponent<BoxCollider2D>().enabled = false;
            playerAnimationController.Goal();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //動く床から離れたら
        if (collision.TryGetComponent(out MobileObstacle mobileObstacle))
        {
            //フラグを折ってベクトルを初期化する
            isMoveGround = false;
            vector = Vector2.zero;

            //移動する床を格納している変数が空ではない且つ
            //離れたオブジェクトと移動する床を格納している変数と同じだったら
            //親子関係を削除して移動する床を格納している変数を初期化
            if (foldObj !=null)
            {
                if(foldObj == mobileObstacle.gameObject)
                {
                    //transform.SetParent(null);
                    foldObj = null;
                }
            }
        }

        //制作途中
        if (collision.gameObject.tag == "Half")
        {
            if(collision.TryGetComponent(out BoxCollider2D collsion2D))
            {
                footCollsion.HalfExit(collsion2D);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //触れているオブジェクトにが移動する床だったら
        if (collision.TryGetComponent(out MobileObstacle mobileObstacle))
        {
            //移動する床の移動ベクトルを取得
            vector = collision.attachedRigidbody.GetPointVelocity(Vector2.zero);
            //内積で移動する床の進行方向にプレイヤーがいるかどうか調べる
            float playerMovementDirection = Vector2.Dot(new Vector2(vector.x, 0).normalized, new Vector2(transform.position.x - collision.transform.position.x, 0).normalized);

            // playerMovementDirectionが正なら1、負なら-1、ゼロなら0
            float result = Mathf.Sign(playerMovementDirection);

            //移動する床の進行方向にプレイヤーがいる又は
            //プレイヤーが移動する床の上に乗っていたら移動する床に合わせて移動する
            if (result == 1 || footCollsion.transform.position.y > collision.gameObject.transform.position.y)
            {
                isMoveGround = true;
                foldObj = mobileObstacle.gameObject;
                //transform.SetParent(mobileObstacle.transform);
            }
            //そうでは無かったら移動しないようにする。
            else if(result == -1)
            {
                isMoveGround= false;
                //ベクトル初期化
                vector = Vector2.zero;
                //transform.SetParent(null);
            }
        }
    }
}
