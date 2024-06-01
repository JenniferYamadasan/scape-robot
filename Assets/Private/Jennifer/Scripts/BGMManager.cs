using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMSTATE
{
    TITLE = 0,
    ENDING = 1,
}
/// <summary>
/// BGMを管理するクラス
/// </summary>
public class BGMManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> clipList;


    void Awake()
    {
        //誤って他のBGMが再生しないように初期化しておく
        audioSource.clip = null;
    }
    /// <summary>
    /// BGMを設定する
    /// </summary>
    /// <param name="bgm"></param>
    public void SetEndBGM(BGMSTATE bgm)
    {
        //音楽を割り当て
        AudioClip clip = clipList[(int)bgm];
        //エンディングとInGameで切り替えるため
        //違うクリップだったら切り替えるそうでない場合は
        //ループしてるため変えない
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
