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
    [SerializeField] private WalkSEPlayer m_walkSEPlayer = null;

    void OnTriggerStay2D(Collider2D collider)
    {
        //地面に触れていたら
        if (collider.gameObject.layer==8 || collider.gameObject.CompareTag("Ground"))
        {
            //プレイヤーコントローラーに地面にいることを伝える(ジャンプ中だったらFlagをtrueにしない)
            if (playerController.rb2D.velocity.y >= 0.1f) return;
            playerController.IsGround(true);
            m_walkSEPlayer.SetIsGround(true);
        }
    }

    /// <summary>
    /// 地面から離れたら
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerExit2D(Collider2D collider)
    {
        //地面から離れたら
        if (collider.gameObject.layer == 8 || collider.gameObject.CompareTag("Ground"))
        {
            //プレイヤーコントローラーに地面にいないを伝える
            playerController.IsGround(false);
            m_walkSEPlayer.SetIsGround(false);
        }
    }
}
