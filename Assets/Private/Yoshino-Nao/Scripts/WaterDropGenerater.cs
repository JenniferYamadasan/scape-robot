using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class RainRandom
{
    /// <summary> ƒ‰ƒ“ƒ_ƒ€‚Ι‰J‚ª‚Σ‚ι‚©‚Η‚¤‚© </summary>
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


    /// <summary>—‚Ώ‚ι…“HƒIƒuƒWƒFƒNƒg</summary>
    [SerializeField] private GameObject m_waterObj = null;
    /// <summary>…“H‚Μ¶¬Κ’u‚Μ•β³’l</summary>
    [SerializeField] private Vector3 m_offsetPos = Vector3.zero;
    /// <summary>…“H‚Μ¶¬•p“x</summary>
    [SerializeField] private float m_dropRate = 1.0f;
    /// <summary>…“H‚Μ“–‚½‚θ”»’θ‚Μ”Όa</summary>
    [SerializeField] private float m_waterRadius = 0.5f;
    /// <summary>…“H‚Μ—‰Ί‘¬“x</summary>
    [SerializeField] private float m_dropSpeed = 1.0f;

    /// <summary>¶¬‚³‚κ‚Δ‚©‚η‚Μo‰ίΤ</summary>
    private float m_elapsedTime = 0f;


    delegate void RainInstance();

    RainInstance rainInstance;

    [Space(30), Header("ƒ‰ƒ“ƒ_ƒ€‚Ε…“H‚π~‚η‚·")]
    [SerializeField] RainRandom rain;

    float randomTime;


    [Space(30),Header("‚±‚±‚©‚ηγY—ν‚Ι•ΐ‚Ρ‚Ε…“H‚π~‚η‚Ή‚ι")]
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
        //o‰ίΤ‚ªκ’θ‚π’΄‚¦‚ι‚Ζ…“H‚π¶¬
        if (m_elapsedTime >= dropTime)
        {
            GameObject WaterObj = Instantiate(m_waterObj, transform.position + m_offsetPos, Quaternion.identity);
            WaterDrop WaterDrop = WaterObj.GetComponent<WaterDrop>();
            //…“H‚Μ—‰Ί‘¬“x‚πέ’θ
            WaterDrop.SetUp(m_waterRadius, m_dropSpeed);
            //¶¬‚Ζ“―‚Ιo‰ίΤ‚πƒƒZƒbƒg
            m_elapsedTime = 0f;
            return true;
        }
        //o‰ίΤ‚Μv‘ª
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
        //…“H‚Μ—‰Ί‘¬“x‚πέ’θ
        WaterDrop.SetUp(m_waterRadius, m_dropSpeed);
    }
}
