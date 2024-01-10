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

    /// <summary>�����蔻����Ǘ����Ă���N���X</summary>
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
                Debug.Log("�c�ׂꂽ");
                Destroy(gameObject);
            }

            if (horizontalFold && Mathf.Abs(throwableObject.vector.x) > 0)
            {
                Debug.Log("���ׂꂽ");
                Destroy(gameObject);
            }
        }
    }
}
