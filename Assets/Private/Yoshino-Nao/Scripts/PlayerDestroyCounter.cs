using TMPro;
using UnityEngine;
public class PlayerDestroyCounter : MonoBehaviour
{
    /// <summary>プレイヤー死亡カウント用テキスト</summary>
    [SerializeField] TextMeshProUGUI m_tmp = null;
    /// <summary>プレイヤー死亡カウント</summary>
    [SerializeField] private static int m_destroyCounter = 0;

    private void Start()
    {
        m_tmp = GetComponentInChildren<TextMeshProUGUI>();
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

    /// <summary>
    /// 死亡した数を返すメソッド
    /// </summary>
    /// <returns></returns>
    public int GetDeathCount() { return m_destroyCounter; }

    /// <summary>
    /// 初めからやり直す際にリセットする。
    /// </summary>
    public void Reset() { m_destroyCounter = 0; }
}
