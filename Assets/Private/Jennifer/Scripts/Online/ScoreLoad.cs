using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.UI;
public class ScoreLoad : MonoBehaviour
{
    [SerializeField] Text text;

    [SerializeField] GameObject textParent;
    public void LoadScore()
    {
        int rank;
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("ScoreClass");
        query.OrderByAscending("score"); // スコアを昇順に並べ替え
        query.Limit = 100;

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            if (e != null)
            {
                Debug.LogWarning("error: " + e.ErrorMessage);
            }
            else
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    rank = i + 1;

                    Text nowText = Instantiate(text);

                    //Debug.Log($"{rank}位		Name: {objList[i]["UserName"]}	スコア: {objList[i]["score"]}");
                    nowText.text = $"{rank}位		Name: {objList[i]["UserName"]}	スコア: {objList[i]["score"]}";

                    nowText.transform.SetParent(textParent.transform);
                }
            }
        });
    }
}