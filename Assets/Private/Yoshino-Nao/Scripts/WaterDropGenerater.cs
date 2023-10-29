using UnityEngine;

public class WaterDropGenerater : MonoBehaviour
{
    /// <summary>落ちる水滴オブジェクト</summary>
    [SerializeField] private GameObject m_waterObj = null;
    /// <summary>水滴の生成位置の補正値</summary>
    [SerializeField] private Vector3 m_offsetPos = Vector3.zero;
    /// <summary>水滴の生成頻度</summary>
    [SerializeField] private float m_dropRate = 1.0f;
    /// <summary>水滴の当たり判定の半径</summary>
    [SerializeField] private float m_waterRadius = 0.5f;
    /// <summary>水滴の落下速度</summary>
    [SerializeField] private float m_dropSpeed = 1.0f;

    /// <summary>生成されてからの経過時間</summary>
    private float m_elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //経過時間が一定を超えると水滴を生成
        if (m_elapsedTime >= m_dropRate)
        {
            GameObject WaterObj = Instantiate(m_waterObj, transform.position + m_offsetPos, Quaternion.identity);
            WaterDrop WaterDrop = WaterObj.GetComponent<WaterDrop>();
            //水滴の落下速度を設定
            WaterDrop.SetUp(m_waterRadius, m_dropSpeed);
            //生成と同時に経過時間をリセット
            m_elapsedTime = 0f;
        }
        //経過時間の計測
        m_elapsedTime += Time.deltaTime;
    }
}
