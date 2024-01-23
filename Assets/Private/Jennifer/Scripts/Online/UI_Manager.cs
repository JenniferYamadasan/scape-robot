using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//https://github.com/NIFCLOUD-mbaas/ncmb_unity/releases
public class UI_Manager : MonoBehaviour
{

    [SerializeField] GameObject ranking_UI;

    [SerializeField] GameObject ranking;

    PlayerDestroyCounter playerDestroyCounter;

    void Start()
    {
        playerDestroyCounter = FindObjectOfType<PlayerDestroyCounter>();
    }
    public void SettingRankingUI()
    {
        ResetUI();
        ranking_UI.SetActive(true);
    }

    public void ResetUI()
    {
        ranking.SetActive(false);
        ranking_UI.SetActive(false);
    }

    public void CloseRankUI()
    {
        ResetUI();
        ranking.SetActive(true);
    }

    /// <summary>
    /// 最初のシーンに戻るメソッド
    /// </summary>
    public void Restart()
    {
        playerDestroyCounter.Reset();
        GameManager.bgmManager.SetEndBGM(BGMSTATE.TITLE);
        SceneManager.LoadScene(0);
    }
}
