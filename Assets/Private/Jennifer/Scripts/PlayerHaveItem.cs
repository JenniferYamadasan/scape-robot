using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHaveItem : MonoBehaviour
{
    public bool itemOwned => hasItem != null;
    public GameObject hasItem;
    public Transform itemPos;
    public Transform hasItemModel;
    [HideInInspector]public BoxCollider2D itemsCollider2D;
    [HideInInspector]public Rigidbody2D itemRB2D;
    [HideInInspector]public ThrowableObject throwableObject;

    void Update()
    {
        //アイテムを持っている間はベクトルを初期化し続ける
        if (hasItem != null)
        {
            hasItem.transform.localPosition = Vector3.zero;
            itemRB2D.velocity = Vector2.zero;
        }
    }
}
