using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollider : MonoBehaviour
{
    /// <summary>
    /// 触れたオブジェクトを格納している変数
    /// </summary>
    List<GameObject> items = new List<GameObject>();

    /// <summary>
    /// プレイヤーのTransForm
    /// </summary>
    [SerializeField] Transform player;
    void OnTriggerStay2D(Collider2D collider2D)
    {
        //リストにアイテムがあるか確認あったら取得
        if(!items.Contains(collider2D.gameObject))
        {
            items.Add(collider2D.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        //リストにアイテムがあるか確認あったら削除
        if (!items.Contains(collider2D.gameObject))
        {
            items.Remove(collider2D.gameObject);
        }
    }
    /// <summary>
    /// 1番近くにあるゲームオブジェクトを返すようにするメソッド
    /// </summary>
    /// <returns></returns>
    public GameObject GetNearestObject()
    {
        if (items.Count == 0) return null;

        //1番近いゲームオブジェクトを格納する変数を宣言
        Transform nearestTransform =null;

        //Listに格納している変数でどれが1番近いか確認している
        for (int i = 0; i < items.Count; i++)
        {
            //最初は1番最初に取得したゲームオブジェクトを格納している
            if (i == 0) nearestTransform = items[0].gameObject.transform;
            else
            {
                //nearestTransformとitems[i]どっちが近いか調べて近い方をnearestTransformに入れるようにしている。
                if (Vector2.Distance(nearestTransform.position, player.position) > Vector2.Distance(items[i].transform.position, player.position))
                {
                    nearestTransform = items[i].transform;
                }
            }
        }
        //1番近くにあったゲームオブジェクト返すようにしている
        return nearestTransform.gameObject;
    }

    /// <summary>
    /// リストの中身を削除
    /// </summary>
    public void ItemReset()
    {
        items.Clear();
    }
}
