//自身を飛ばすクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{

    /// <summary> 自身のリジットボディ </summary>
    [SerializeField] Rigidbody2D rb2D;

    /// <summary> 飛ばす力 </summary>
    [SerializeField] float power;

    /// <summary> 飛ばす方角 </summary>
    [SerializeField,Tooltip("Y座標とばす距離調整用")]float yVec;

    /// <summary> 持っているオブジェクトを飛ばすベクトル </summary>
    Vector2 flightDirection;

    /// <summary> プレイヤーが現在持っているアイテムを格納しているclass </summary>
    ItemCollider itemCollider;

    /// <summary> x軸に飛ばす距離 </summary>
    [SerializeField] float xPower;

    /// <summary> キャラクターのモデルを格納している変数 </summary>
    [SerializeField] Transform Model;

    /// <summary>
    /// 右向いているときと左向いている時の回転の値
    /// </summary>
    [SerializeField] List<Vector3> rotation = new List<Vector3>();

    /// <summary> プレイヤーに触れているかどうか </summary>
    bool isTouchingSpecificObject = false;

    /// <summary> 自身のコライダーを格納している </summary>
    [SerializeField] BoxCollider2D throwCollider;

    /// <summary> 現在投げられているかどうか </summary>
    public bool isThrow = false;

    /// <summary> 動く床に触れているかどうか </summary>
    bool isMoveGround = false;

    /// <summary>現在どの方向に押されているか</summary>
    [field:SerializeField]public Vector2 vector { get; private set; }

    /// <summary> 自身の足のポジション </summary>
    [SerializeField] Transform footPos;

    /// <summary> アイテムを持っているかどうか </summary>
    public bool hasItem;

    /// <summary> 地面に触れているかどうか </summary>
    bool isGround = false;

    void Start()
    {
        //プレイヤーのアイテムコライダーを取得
        itemCollider = FindObjectOfType<ItemCollider>();
        //物理挙動を有効にする
        rb2D.isKinematic = false;
    }

    /// <summary>
    /// キャラクターのモデルを回転させるメソッド
    /// </summary>
    /// <param name="direction"></param>
    public void OnRotate(DIRECTION direction)
    {
        //キャラクターを回転させる
        Model.rotation = Quaternion.Euler(rotation[(int)direction]);
    }
    /// <summary>
    /// このメソッドで物を飛ばしている
    /// </summary>
    public void OnThrow(DIRECTION direction)
    {
        //移動ベクトルを初期化している
        VeclocityReset();
        //キャラクターの向いている方向に対して飛ばす方向を変更している
        if(direction == DIRECTION.RIGHT)
        {
            flightDirection = new Vector2(xPower, yVec) * power;
        }
        else
        {
            flightDirection = new Vector2(-xPower, yVec) * power;
        }
        //実際に飛ばす
        rb2D.AddForce(flightDirection, ForceMode2D.Impulse);
        //オブジェクトを掴んだ瞬間にZ軸が前に来ている為投げた瞬間にZ軸を元の場所に戻している
        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        //投げているのでisThrowをtrueにしている
        isThrow = true;
        //持っているアイテムを削除する。
        itemCollider.ItemReset();
    }

    void Update()
    {
        //移動する床に触れている且つアイテムを持っていない場合にオブジェクトを移動させる(移動する床によって押される)
        if (isMoveGround && !hasItem) rb2D.velocity = new Vector2(vector.x, 0) + new Vector2(0, rb2D.velocity.y);
        //移動する床に触れていない且つ
        //移動しない床に触れている且つ
        //アイテムが投げられていない且つ
        //アイテムを持っていない場合X軸の固定と回転の固定をしている
        if (!isMoveGround && (isGround && !isThrow && !hasItem)) rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        //それ以外だったら回転だけ固定する
        else { rb2D.constraints = RigidbodyConstraints2D.FreezeRotation; }

     
        isTouchingSpecificObject = false; // フレームの開始時にリセット

        // オブジェクトを持ち上げた際にOnTriggerExitが反応しなくなる為、ここで衝突判定を行っている
        Collider2D[] colliders = Physics2D.OverlapBoxAll(throwCollider.bounds.center, throwCollider.size, 0);
        //現在床に触れているかどうか
        bool isTouchingGround = false;
        foreach (Collider2D col in colliders)
        {
            //触れたオブジェクトによってオブジェクトのベクトルを初期化したり、物理挙動を無効化したりしている
            if ((col.gameObject.tag == "Player" && this.gameObject != col) &&!isThrow)
            {
                if (isMoveGround) break;
                OnStopOrExit(true);
                isTouchingSpecificObject = true;
                break; // 一つでも特定のオブジェクトに触れていれば終了
            }
            
            //地面にふれたか
            if(col.gameObject.tag == "Ground")
            {
                isTouchingGround = true;
            }
        }
        //地面に触れていることを伝える
        isGround = isTouchingGround;
        //地面にふれていない場合物理挙動を有効にする
        if (!isTouchingSpecificObject) OnExit(false);
    }

    /// <summary>
    /// ベクトルの初期化、物理挙動の有効か無効化を行っている
    /// </summary>
    /// <param name="result"></param>
    public void OnStopOrExit(bool result)
    {
        //投げた物を滑らないようにしている
        //flightDirection = rb2D.velocity = new Vector2(0, 0);
        rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    /// <summary>
    /// 物理挙動の有効化、無効化
    /// </summary>
    /// <param name="result"></param>
    public void OnExit(bool result)
    {
        rb2D.isKinematic= result;
    }

    /// <summary>
    /// 移動ベクトルの初期化
    /// </summary>
    public void VeclocityReset()
    {
        rb2D.velocity = Vector2.zero;
    }


    void OnTriggerStay2D(Collider2D collision)
    {
        //動く床に触れているかどうか
        if (collision.TryGetComponent(out MobileObstacle mobileObstacle))
        {
            //触れている場合動く床の移動ベクトルを取得
            vector = collision.attachedRigidbody.GetPointVelocity(Vector2.zero);
            //触れたオブジェクトと自身の内積を計算している
            float playerMovementDirection = Vector2.Dot(new Vector2(vector.x, 0).normalized, new Vector2(transform.position.x - collision.transform.position.x, 0).normalized);

            // playerMovementDirectionが正なら1、負なら-1、ゼロなら0
            float result = Mathf.Sign(playerMovementDirection);

            //動く床の進行方向に自身がいる又は
            //自身が動く床の上に乗っている場合自身が勝手に動くようにしたいのでFlagを立てている
            if (result == 1 || footPos.position.y > collision.gameObject.transform.position.y)
            {
                isMoveGround = true;

                rb2D.isKinematic = false;

            }
            //そうではない場合フラグを折る
            else if(result == -1)
            {
                isMoveGround = false;

                vector = Vector2.zero;
            }
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out MobileObstacle mobileObstacle))
        {
            isMoveGround = false;
            vector = Vector2.zero;
        }
    }

}
