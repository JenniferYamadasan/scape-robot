//自身を飛ばすクラス

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{

    /// <summary>
    /// 自身のリジットボディ
    /// </summary>
    [SerializeField] Rigidbody2D rb2D;

    /// <summary>
    /// 飛ばす力
    /// </summary>
    [SerializeField] float power;

    /// <summary>
    /// 飛ばす方角
    /// </summary>
    [SerializeField,Tooltip("Y座標とばす距離調整用")]float yVec;

    Vector2 flightDirection;

    ItemCollider itemCollider;

    public Vector3 pos { get; private set; }

    [SerializeField] float xPower;

    [SerializeField] Transform Model;

    [SerializeField] List<Vector3> rotation = new List<Vector3>();

    bool isTouchingSpecificObject = false;

    PlayerHaveItem playerHaveItem;

    [SerializeField] BoxCollider2D throwCollider;

    public bool isThrow = false;

    bool isMoveGround = false;

    Vector2 vector;

    void Start()
    {
        playerHaveItem = FindObjectOfType<PlayerHaveItem>();
        itemCollider = FindObjectOfType<ItemCollider>();
        rb2D.isKinematic = false;
        pos = this.transform.position;
    }

    public void OnRotate(DIRECTION direction)
    {
        Model.rotation = Quaternion.Euler(rotation[(int)direction]);
    }
    /// <summary>
    /// このメソッドで物を飛ばしている
    /// </summary>
    public void OnThrow(DIRECTION direction)
    {
        VeclocityReset();
        if(direction == DIRECTION.RIGHT)
        {
            flightDirection = new Vector2(xPower, yVec) * power;
        }
        else
        {
            flightDirection = new Vector2(-xPower, yVec) * power;
        }
        rb2D.AddForce(flightDirection, ForceMode2D.Impulse);
        transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        isThrow = true;
        itemCollider.ItemReset();
    }

    void Update()
    {

        //if (isMoveGround) rb2D.velocity = new Vector2(vector.x, vector.y) + new Vector2(0, rb2D.velocity.y);
        isTouchingSpecificObject = false; // フレームの開始時にリセット

        // オブジェクトを持ち上げた際にOnTriggerExitが反応しなくなる為、ここで衝突判定を行っている
        Collider2D[] colliders = Physics2D.OverlapBoxAll(throwCollider.bounds.center, throwCollider.size, 0);
        foreach (Collider2D col in colliders)
        {
            if ((col.gameObject.tag == "Player" && this.gameObject != col) &&!isThrow)
            {
                OnStopOrExit(true);
                isTouchingSpecificObject = true;
                break; // 一つでも特定のオブジェクトに触れていれば終了
            }
        }

        if (!isTouchingSpecificObject) OnExit(false);
    }

    public void OnStopOrExit(bool result)
    {
        //投げた物を滑らないようにしている
        flightDirection = rb2D.velocity = new Vector2(0, 0);
        rb2D.isKinematic = result;
    }

    
    public void OnExit(bool result)
    {
        rb2D.isKinematic= result;
    }

    public void VeclocityReset()
    {
        rb2D.velocity = Vector2.zero;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MobileObstacle mobileObstacle))
        {
            isMoveGround = true;

            rb2D.isKinematic = false;

            vector = collision.attachedRigidbody.GetPointVelocity(Vector2.zero);

            Debug.Log(vector);
        }


    }

}
