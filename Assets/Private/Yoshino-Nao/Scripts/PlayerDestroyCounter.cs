using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDestroyCounter : MonoBehaviour
{
    /// <summary>�v���C���[���S�J�E���g�p�e�L�X�g</summary>
    [SerializeField] TextMeshProUGUI m_tmp = null;
    /// <summary>�v���C���[���S�J�E���g</summary>
    [SerializeField] private int m_destroyCounter = 0;

    private void Start()
    {
        m_tmp = GetComponentInChildren<TextMeshProUGUI>();
        m_destroyCounter = 0;
    }
    private void Update()
    {
        if (m_tmp != null)
        {
            m_tmp.text = m_destroyCounter.ToString();
        }
    }
    /// <summary>���S�J�E���g�����Z</summary>
    public void DestroyCounterAdd()
    {
        m_destroyCounter++;
    }
    /// <summary>���S�J�E���g�����Z�b�g</summary>
    /// <param name="value">�������l</param>
    public void DestroyCounterReset(int value)
    {
        m_destroyCounter = value;
    }
}
