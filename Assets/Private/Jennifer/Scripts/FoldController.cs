using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldController : MonoBehaviour
{
    #region 当たり判定のclass
    /// <summary>
    /// 当たり判定をclassでまとめている
    /// </summary>
    [System.Serializable]
    public class FoldCollisionManager
    {
        [field : SerializeField] public FoldCollision upCollsion { get; private set; }
        [field : SerializeField] public FoldCollision leftCollsion { get; private set; }
        [field : SerializeField] public FoldCollision rightCollsion { get; private set; }
        [field : SerializeField] public FoldCollision downCollsion { get; private set; }
    }
    #endregion

    /// <summary>当たり判定を管理しているクラス</summary>
    [SerializeField] FoldCollisionManager foldManager;

    /// <summary> 親オブジェクトについているclass </summary>
    [SerializeField] ThrowableObject throwableObject;

    /// <summary> 上下にオブジェクトが挟まれているかどうか </summary>
    bool verticalFold => foldManager.upCollsion.isCollsion && foldManager.downCollsion.isCollsion;

    /// <summary> 左右にオブジェクトが挟まれているかどうか </summary>
    bool horizontalFold => foldManager.rightCollsion.isCollsion && foldManager.leftCollsion.isCollsion;


    void Update()
    {
        //地面に触れている且つ
        //頭と足にオブジェクトがぶつかっている且つ
        //自身のオブジェクトが縦移動している場合に潰れるようにする。
        if(foldManager.downCollsion.isCollsion)
        {
            if (verticalFold && Mathf.Abs(throwableObject.vector.y) > 0)
            {
                Debug.Log("縦潰れた");
                Destroy(gameObject);
            }
        }

        //左と右にオブジェクトがぶつかっている且つ
        //地震のオブジェクトが横に移動している場合潰れるようにする。
        if (horizontalFold && Mathf.Abs(throwableObject.vector.x) > 0)
        {
            Debug.Log("横潰れた");
            Destroy(gameObject);
        }
    }
}
