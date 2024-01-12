using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldController : MonoBehaviour
{
    #region �����蔻���class
    /// <summary>
    /// �����蔻���class�ł܂Ƃ߂Ă���
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

    /// <summary>�����蔻����Ǘ����Ă���N���X</summary>
    [SerializeField] FoldCollisionManager foldManager;

    /// <summary> �e�I�u�W�F�N�g�ɂ��Ă���class </summary>
    [SerializeField] ThrowableObject throwableObject;

    /// <summary> �㉺�ɃI�u�W�F�N�g�����܂�Ă��邩�ǂ��� </summary>
    bool verticalFold => foldManager.upCollsion.isCollsion && foldManager.downCollsion.isCollsion;

    /// <summary> ���E�ɃI�u�W�F�N�g�����܂�Ă��邩�ǂ��� </summary>
    bool horizontalFold => foldManager.rightCollsion.isCollsion && foldManager.leftCollsion.isCollsion;


    void Update()
    {
        //�n�ʂɐG��Ă��銎��
        //���Ƒ��ɃI�u�W�F�N�g���Ԃ����Ă��銎��
        //���g�̃I�u�W�F�N�g���c�ړ����Ă���ꍇ�ɒׂ��悤�ɂ���B
        if(foldManager.downCollsion.isCollsion)
        {
            if (verticalFold && Mathf.Abs(throwableObject.vector.y) > 0)
            {
                Debug.Log("�c�ׂꂽ");
                Destroy(gameObject);
            }
        }

        //���ƉE�ɃI�u�W�F�N�g���Ԃ����Ă��銎��
        //�n�k�̃I�u�W�F�N�g�����Ɉړ����Ă���ꍇ�ׂ��悤�ɂ���B
        if (horizontalFold && Mathf.Abs(throwableObject.vector.x) > 0)
        {
            Debug.Log("���ׂꂽ");
            Destroy(gameObject);
        }
    }
}
