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

    public bool isHalf { get; private set; } = false; 
    List<BoxCollider2D> halfColliders = new List<BoxCollider2D>();

    [field:SerializeField] public BoxCollider2D footCollsion { get; private set; }

    void OnTriggerStay2D(Collider2D collider)
    {
        //地面に触れていたら
        if (collider.gameObject.layer==8 || collider.gameObject.CompareTag("Ground"))
        {
            //プレイヤーコントローラーに地面にいることを伝える(ジャンプ中だったらFlagをtrueにしない)
            //if (playerController.rb2D.velocity.y >= 0.1f) return;
            playerController.IsGround(true);
            m_walkSEPlayer.SetIsGround(true);
        }

        if(collider.gameObject.tag=="Half")
        {
            isHalf = true;
            if(collider.TryGetComponent(out BoxCollider2D halfCollider))
            {
                if(!halfColliders.Contains(halfCollider)) halfColliders.Add(halfCollider);

            }
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

    public void HalfTrigger()
    {
        foreach (var item in halfColliders)
        {
            item.isTrigger = true;
        }
    }

    public void HalfExit(BoxCollider2D halfCollider)
    {
        if (halfColliders.Contains(halfCollider))
        {
            halfCollider.isTrigger = false;
            halfColliders.Remove(halfCollider);
        }
    }
}
