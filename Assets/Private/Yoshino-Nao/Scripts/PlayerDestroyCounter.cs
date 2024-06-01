using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerDestroyCounter : MonoBehaviour
{
    /// <summary>プレイヤーの総破壊カウント</summary>
    [SerializeField] private static int m_destroyCounter;

    /// <summary>カウンターのMeshFilterリスト</summary>
    [SerializeField] private List<MeshFilter> counter = new List<MeshFilter>();

    /// <summary>数字メッシュのリスト</summary>
    [SerializeField] private List<Mesh> meshNumber = new List<Mesh>();

    /// <summary>スコアオブジェクト</summary>
    [SerializeField] private GameObject scoreObjct;

    void Start()
    {
        if (scoreObjct != null) return;
        UIUpdate();
    }

    /// <summary>破壊カウントを追加する</summary>
    public void DestroyCounterAdd()
    {
        m_destroyCounter++;
        UIUpdate();
    }

    /// <summary>UIを更新する</summary>
    void UIUpdate()
    {
        int hundredsPlace = m_destroyCounter / 100;//百の位取得
        int tensPlace = (m_destroyCounter - (hundredsPlace * 100)) / 10;//十の位取得
        int unitsPlace = (m_destroyCounter - (hundredsPlace * 100 + tensPlace * 10));//一の位取得

        //それぞれUIに割り当てる
        counter[0].mesh = meshNumber[unitsPlace];
        counter[1].mesh = meshNumber[tensPlace];
        counter[2].mesh = meshNumber[hundredsPlace];
    }

    /// <summary>UIをリセットする</summary>
    public void UIReset()
    {
        foreach (MeshFilter item in counter)
        {
            item.mesh = meshNumber[0];
        }
    }

    /// <summary>アイテムの評価を行う</summary>
    /// <param name="scoreTime">スコア表示時間</param>
    public IEnumerator AppraiseItem(float scoreTime)
    {
        int hundredsPlace = m_destroyCounter / 100;
        int tensPlace = (m_destroyCounter - (hundredsPlace * 100)) / 10;
        int unitsPlace = (m_destroyCounter - (hundredsPlace * 100 + tensPlace * 10));

        string valueString = hundredsPlace.ToString() + tensPlace.ToString() + unitsPlace.ToString();

        for (int i = 0; i < valueString.Length; i++)
        {
            int nowcount = (counter.Count - 1) - i;
            StartCoroutine(AnimateDigit(i, nowcount, valueString));
            yield return new WaitForSeconds(1); // 1秒待つ
        }
        yield return new WaitForSeconds(scoreTime);
        scoreObjct.SetActive(true);
    }

    /// <summary>桁をアニメーションで表示する</summary>
    /// <param name="digitIndex">桁のインデックス</param>
    /// <param name="now">現在の桁</param>
    /// <param name="score">スコア文字列</param>
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
                yield return new WaitForSeconds(0.02f); // 0.02秒待つ
            }
        }
        counter[digitIndex].mesh = meshNumber[int.Parse(score[now].ToString())];
    }

    /// <summary>破壊カウントを取得する</summary>
    /// <returns>破壊カウント</returns>
    public int GetDeathCount() { return m_destroyCounter; }

    /// <summary>破壊カウントをリセットする</summary>
    public void Reset() { m_destroyCounter = 0; }
}