using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerDestroyCounter : MonoBehaviour
{
    /// <summary>プレイヤー死亡カウント</summary>
    [SerializeField] private static int m_destroyCounter;

    [SerializeField] List<MeshFilter> counter = new List<MeshFilter>();
    
    [SerializeField] List<Mesh> meshNumber = new List<Mesh>();

    [SerializeField] GameObject scoreObjct;


    void Start()
    {
        if (scoreObjct != null) return;
        UIUpdate();
    }

    /// <summary>死亡カウントを加算</summary>
    public void DestroyCounterAdd()
    {
        m_destroyCounter++;
        UIUpdate();
    }

    void UIUpdate()
    {
        int hundredsPlace = m_destroyCounter / 100;
        int tensPlace = (m_destroyCounter - (hundredsPlace * 100)) / 10;
        int unitsPlace = (m_destroyCounter - (hundredsPlace * 100 + tensPlace * 10));

        //Debug.Log($"hundredsPlace = {hundredsPlace} tensPlace = {tensPlace} unitsPlace = {unitsPlace}");
        counter[0].mesh = meshNumber[unitsPlace];
        counter[1].mesh = meshNumber[tensPlace];
        counter[2].mesh = meshNumber[hundredsPlace];

    }

    public void UIReset()
    {
        foreach (MeshFilter item in counter)
        {
            item.mesh = meshNumber[0]; 
        }
    }
    public IEnumerator AppraiseItem(float scoreTime)
    {

        int hundredsPlace = m_destroyCounter / 100;
        int tensPlace = (m_destroyCounter - (hundredsPlace * 100)) / 10;
        int unitsPlace = (m_destroyCounter - (hundredsPlace * 100 + tensPlace * 10));

        string valueString = hundredsPlace.ToString() + tensPlace.ToString() + unitsPlace.ToString() ;

        for (int i = 0; i < valueString.Length; i++)
        {
            int nowcount = (counter.Count - 1) - i;
            StartCoroutine(AnimateDigit(i, nowcount, valueString));
            yield return new WaitForSeconds(1); // 1秒待つ
        }
        yield return new WaitForSeconds(scoreTime);
        scoreObjct.SetActive(true);
    }

    IEnumerator AnimateDigit(int digitIndex, int now, string score)
    {
        for (int n = 0; n < 5; n++)
        {
            for (int i = 0; i < 10; i++)
            {
                if (now == 2)
                {
                    counter[2].mesh = meshNumber[i];
                    counter[1].mesh = meshNumber[i];
                    counter[0].mesh = meshNumber[i];
                }
                if (now == 1)
                {
                    counter[2].mesh = meshNumber[i];
                    counter[1].mesh = meshNumber[i];
                }
                if (now == 0)
                {
                    counter[2].mesh = meshNumber[i];
                }
                yield return new WaitForSeconds(0.02f); // 0.1秒待つ
            }
        }
        counter[digitIndex].mesh = meshNumber[int.Parse(score[now].ToString())];
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
