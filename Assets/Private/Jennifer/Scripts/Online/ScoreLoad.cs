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
        query.OrderByAscending("score"); // �X�R�A�������ɕ��בւ�
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

                    //Debug.Log($"{rank}��		Name: {objList[i]["UserName"]}	�X�R�A: {objList[i]["score"]}");
                    nowText.text = $"{rank}��		Name: {objList[i]["UserName"]}	�X�R�A: {objList[i]["score"]}";

                    nowText.transform.SetParent(textParent.transform);
                }
            }
        });
    }
}