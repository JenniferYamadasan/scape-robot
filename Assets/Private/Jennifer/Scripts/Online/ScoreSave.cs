using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
public class ScoreSave : MonoBehaviour
{
    [SerializeField] Loginsignin loginsignin;

    [SerializeField] ScoreLoad scoreLoad;
    public void SaveScore(int Score, string userName)
    {
        NCMBObject scoreClass = new NCMBObject("ScoreClass");
        scoreClass["score"] = Score;
        scoreClass["UserName"] = userName;
        scoreClass.SaveAsync((NCMBException e) =>
        {
            if (e != null)
            {
                Debug.Log("Error: " + e.ErrorMessage);
            }
            else
            {
                Debug.Log("success");
            }
        });
        scoreLoad.LoadScore();
    }
}