using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 雨のランダム性を管理するクラス
/// </summary>
[Serializable]
public class RainRandom
{
    /// <summary> 雨をランダムに降らせるかどうか </summary>
    [field: SerializeField] public bool isRandom { get; private set; }

    /// <summary> ランダムな時間の最小値 </summary>
    [field: SerializeField] public float min { get; private set; }

    /// <summary> ランダムな時間の最大値 </summary>
    [field: SerializeField] public float max { get; private set; }
}

/// <summary>
/// 雨の種類を管理するクラス
/// </summary>
[Serializable]
public class RainType
{
    /// <summary> 雨のインスタンスを配置する位置 </summary>
    [field: SerializeField] public Transform posInstance { get; private set; }

    /// <summary> 雨が降る時間間隔 </summary>
    [field: SerializeField] public float dropTime { get; private set; }
}

/// <summary>
/// 水滴を生成するクラス
/// </summary>
public class WaterDropGenerater : MonoBehaviour
{
    /// <summary> 水滴オブジェクト </summary>
    [SerializeField] private GameObject m_waterObj = null;

    /// <summary> 水滴の初期位置のオフセット </summary>
    [SerializeField] private Vector3 m_offsetPos = Vector3.zero;

    /// <summary> 水滴の降る頻度 </summary>
    [SerializeField] private float m_dropRate = 1.0f;

    /// <summary> 水滴の生成範囲半径 </summary>
    [SerializeField] private float m_waterRadius = 0.5f;

    /// <summary> 水滴の落下速度 </summary>
    [SerializeField] private float m_dropSpeed = 1.0f;

    /// <summary> 経過時間 </summary>
    private float m_elapsedTime = 0f;

    // 雨のインスタンスを管理するデリゲート
    private delegate void RainInstance();
    private RainInstance rainInstance;

    [Space(30), Header("ランダムな雨を有効にする")]
    [SerializeField] private RainRandom rain;

    private float randomTime;

    [Space(30), Header("連続的に雨を降らせる")]
    [SerializeField] private bool isEmmiter;

    [SerializeField] private List<RainType> rainTypes = new List<RainType>();

    void Start()
    {
        if (isEmmiter)
        {
            rainInstance = BeginRainContinuous;
        }
        else
        {
            if (rain.isRandom)
            {
                rainInstance = RainRandomDrop;
                randomTime = UnityEngine.Random.Range(rain.min, rain.max);
            }
            else
            {
                rainInstance = Normal;
            }
        }
    }

    void Update()
    {
        rainInstance();
    }

    /// <summary>
    /// 通常の雨の降らせ方
    /// </summary>
    void Normal()
    {
        RainInstanceMethod(m_dropRate);
    }

    /// <summary>
    /// ランダムな時間間隔で雨を降らせる
    /// </summary>
    void RainRandomDrop()
    {
        if (RainInstanceMethod(randomTime))
        {
            randomTime = UnityEngine.Random.Range(rain.min, rain.max);
        }
    }

    /// <summary>
    /// 雨のインスタンスを生成するメソッド
    /// </summary>
    /// <param name="dropTime">雨が降る時間間隔</param>
    /// <returns>インスタンスが生成されたかどうか</returns>
    bool RainInstanceMethod(float dropTime)
    {
        if (m_elapsedTime >= dropTime)
        {
            GameObject waterObj = Instantiate(m_waterObj, transform.position + m_offsetPos, Quaternion.identity);
            WaterDrop waterDrop = waterObj.GetComponent<WaterDrop>();
            waterDrop.SetUp(m_waterRadius, m_dropSpeed);
            m_elapsedTime = 0f;
            return true;
        }
        m_elapsedTime += Time.deltaTime;
        return false;
    }

    /// <summary>
    /// 連続的に雨を降らせるメソッド
    /// </summary>
    void BeginRainContinuous()
    {
        if (RainInstanceMethod(m_dropRate))
        {
            foreach (RainType rainType in rainTypes)
            {
                StartCoroutine(Drop(rainType.dropTime, rainType.posInstance.position));
            }
        }
    }

    /// <summary>
    /// 指定した時間後に水滴を生成するコルーチン
    /// </summary>
    /// <param name="time">待機時間</param>
    /// <param name="pos">生成位置</param>
    /// <returns></returns>
    IEnumerator Drop(float time, Vector3 pos)
    {
        yield return new WaitForSeconds(time);
        GameObject waterObj = Instantiate(m_waterObj, pos + m_offsetPos, Quaternion.identity);
        WaterDrop waterDrop = waterObj.GetComponent<WaterDrop>();
        waterDrop.SetUp(m_waterRadius, m_dropSpeed);
    }
}