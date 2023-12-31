using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class RainRandom
{
    /// <summary> ランダムに雨がふるかどうか </summary>
    [field: SerializeField] public bool isRandom { get; private set; }

    [field: SerializeField] public float min { get; private set; }
    [field: SerializeField] public float max { get; private set; }
}

[Serializable]
public class RainType
{
    [field: SerializeField] public Transform posInstance { get; private set; }
    [field: SerializeField] public float dropTime { get; private set; }
}

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


    delegate void RainInstance();

    RainInstance rainInstance;

    [Space(30), Header("ランダムで水滴を降らす")]
    [SerializeField] RainRandom rain;

    float randomTime;


    [Space(30),Header("ここから綺麗に並びで水滴を降らせる")]
    [SerializeField] bool isEmmiter;
    [SerializeField] List<RainType> rainTypes = new List<RainType>();
    void Start()
    {
        if(isEmmiter)
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
            else { rainInstance = Normal; }
        }
    }

    void Update()
    {
        rainInstance();
    }

    void Normal()
    {
        RainInstanceMethod(m_dropRate);
    }

    void RainRandomDrop()
    {
        if (RainInstanceMethod(randomTime))
        {
            randomTime = UnityEngine.Random.Range(rain.min, rain.max);
        }
    }


    bool RainInstanceMethod(float dropTime)
    {
        //経過時間が一定を超えると水滴を生成
        if (m_elapsedTime >= dropTime)
        {
            GameObject WaterObj = Instantiate(m_waterObj, transform.position + m_offsetPos, Quaternion.identity);
            WaterDrop WaterDrop = WaterObj.GetComponent<WaterDrop>();
            //水滴の落下速度を設定
            WaterDrop.SetUp(m_waterRadius, m_dropSpeed);
            //生成と同時に経過時間をリセット
            m_elapsedTime = 0f;
            return true;
        }
        //経過時間の計測
        m_elapsedTime += Time.deltaTime;
        return false;

    }

    void BeginRainContinuous()
    {
        if(RainInstanceMethod(m_dropRate))
        {
            for (int i = 0; i < rainTypes.Count; i++)
            {
                StartCoroutine(drop(rainTypes[i].dropTime, rainTypes[i].posInstance.position));
            }
        }
    }

    IEnumerator drop(float time,Vector3 pos)
    {
        yield return new WaitForSeconds(time);
        GameObject WaterObj = Instantiate(m_waterObj, pos + m_offsetPos, Quaternion.identity);
        WaterDrop WaterDrop = WaterObj.GetComponent<WaterDrop>();
        //水滴の落下速度を設定
        WaterDrop.SetUp(m_waterRadius, m_dropSpeed);
    }
}
