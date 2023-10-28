using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollider : MonoBehaviour
{
    /// <summary>
    /// 触れたオブジェクトを格納している変数
    /// </summary>
    List<GameObject> items = new List<GameObject>();


    private void OnTriggerStay2D(Collider2D collider2D)
    {
        //リストにアイテムがあるか確認あったら取得
        if(!items.Contains(collider2D.gameObject))
        {
            items.Add(collider2D.gameObject);
        }
    }
}
