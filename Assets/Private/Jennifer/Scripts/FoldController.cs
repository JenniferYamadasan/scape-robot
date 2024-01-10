using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldController : MonoBehaviour
{

    [System.Serializable]
    public class FoldCollisionManager
    {
        [field : SerializeField] public FoldCollision upCollsion { get; private set; }
        [field : SerializeField] public FoldCollision leftCollsion { get; private set; }
        [field : SerializeField] public FoldCollision rightCollsion { get; private set; }
        [field : SerializeField] public FoldCollision downCollsion { get; private set; }
    }

    /// <summary>当たり判定を管理しているクラス</summary>
    [SerializeField] FoldCollisionManager foldManager;

    [SerializeField] ThrowableObject throwableObject;
    bool verticalFold => foldManager.upCollsion.isCollsion && foldManager.downCollsion.isCollsion;

    bool horizontalFold => foldManager.rightCollsion.isCollsion && foldManager.leftCollsion.isCollsion;


    void Update()
    {
        if(foldManager.downCollsion.isCollsion)
        {
            if (verticalFold && Mathf.Abs(throwableObject.vector.y) > 0)
            {
                Debug.Log("縦潰れた");
                Destroy(gameObject);
            }

            if (horizontalFold && Mathf.Abs(throwableObject.vector.x) > 0)
            {
                Debug.Log("横潰れた");
                Destroy(gameObject);
            }
        }
    }
}
