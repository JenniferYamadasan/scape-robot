using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    /// <summary>
    /// 触れたオブジェクトを格納している変数
    /// </summary>
    List<GameObject> items = new List<GameObject>();

    [SerializeField] float jumpPower;

    public void AddPower()
    {
        foreach (GameObject item in items)
        {
            if(item.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D))
            {
                rb2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            }
        }
        ItemReset();
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        //リストにアイテムがあるか確認あったら取得
        if (!items.Contains(collider2D.transform.parent.gameObject))
        {
            items.Add(collider2D.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        //リストにアイテムがあるか確認あったら削除
        if (items.Contains(collider2D.transform.parent.gameObject))
        {
            items.Remove(collider2D.transform.parent.gameObject);
        }
    }

    /// <summary>
    /// リストの中身を削除
    /// </summary>
    public void ItemReset()
    {
        if (items.Count == 0) return;
        items.Clear();
    }

}
