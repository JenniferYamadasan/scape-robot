using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDestroyCounter : MonoBehaviour
{
    /// <summary>プレイヤー死亡カウント用テキスト</summary>
    [SerializeField] TextMeshProUGUI m_tmp = null;
    /// <summary>プレイヤー死亡カウント</summary>
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
    /// <summary>死亡カウントを加算</summary>
    public void DestroyCounterAdd()
    {
        m_destroyCounter++;
    }
    /// <summary>死亡カウントをリセット</summary>
    /// <param name="value">初期化値</param>
    public void DestroyCounterReset(int value)
    {
        m_destroyCounter = value;
    }
}
