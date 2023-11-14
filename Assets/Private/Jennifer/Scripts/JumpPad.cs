using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    /// <summary>
    /// �G�ꂽ�I�u�W�F�N�g���i�[���Ă���ϐ�
    /// </summary>
    List<GameObject> items = new List<GameObject>();

    [SerializeField] float jumpPower;

    public void AddPower()
    {
        foreach (GameObject item in items)
        {
            if(item.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D))
            {
                rb2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Force);
            }
        }
        ItemReset();
    }

    void OnTriggerStay2D(Collider2D collider2D)
    {
        //���X�g�ɃA�C�e�������邩�m�F��������擾
        if (!items.Contains(collider2D.transform.parent.gameObject))
        {
            items.Add(collider2D.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        //���X�g�ɃA�C�e�������邩�m�F��������폜
        if (items.Contains(collider2D.transform.parent.gameObject))
        {
            items.Remove(collider2D.transform.parent.gameObject);
        }
    }

    /// <summary>
    /// ���X�g�̒��g���폜
    /// </summary>
    public void ItemReset()
    {
        if (items.Count == 0) return;
        items.Clear();
    }

}
