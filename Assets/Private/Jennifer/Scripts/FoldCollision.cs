using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldCollision : MonoBehaviour
{
    // 衝突が発生しているかどうかのフラグ
    public bool isCollsion { get; private set; } = false;

    // BoxCollider2Dの参照
    [SerializeField] private BoxCollider2D collider2D;

    // 衝突しているオブジェクト
    public GameObject collsionGameObject;

    void Update()
    {
        // オブジェクトがトリガーから出たかを判定するための処理
        Collider2D[] colliders = Physics2D.OverlapBoxAll(collider2D.bounds.center, collider2D.size, 0);

        // 現在地面に接触しているかどうか
        bool isTouchingGround = false;

        // すべての検出されたCollider2Dをチェック
        foreach (Collider2D col in colliders)
        {
            // 検出されたオブジェクトがGroundタグを持ち、かつレイヤーが8の場合
            if (col.gameObject.CompareTag("Ground") && col.gameObject.layer == 8)
            {
                collsionGameObject = col.gameObject;
                isTouchingGround = true;
            }
        }

        // 接触状態を更新
        isCollsion = isTouchingGround;
    }
}