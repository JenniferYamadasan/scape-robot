//このクラスで地面に足がついているか確かめている


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollsion : MonoBehaviour
{

    /// <summary>
    /// プレイヤーの挙動を管理するスクリプトを格納
    /// </summary>
    [SerializeField] PlayerController playerController;
    void OnTriggerStay2D(Collider2D collider)
    {
        //地面に触れていたら
        if (collider.gameObject.CompareTag("Ground"))
        {
            //プレイヤーコントローラーに地面にいることを伝える
            playerController.IsGround(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //地面から離れたら
        if (collider.gameObject.CompareTag("Ground"))
        {
            //プレイヤーコントローラーに地面にいないを伝える
            playerController.IsGround(false);
        }
    }
}
