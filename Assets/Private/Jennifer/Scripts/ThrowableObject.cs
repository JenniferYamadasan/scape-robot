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

    void Start()
    {
        itemCollider = FindObjectOfType<ItemCollider>();
        rb2D.isKinematic = false;
        pos = this.transform.position;
    }

    /// <summary>
    /// このメソッドで物を飛ばしている
    /// </summary>
    public void OnThrow(DIRECTION direction)
    {
        Debug.Log("direction"+direction);
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

        itemCollider.ItemReset();
    }

    public void OnStop()
    {
        //投げた物を滑らないようにしている
        flightDirection = rb2D.velocity = new Vector2(0, 0);
        rb2D.isKinematic = true;
    }
}
