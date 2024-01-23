using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> clipList;

    void Awake()
    {
        audioSource.clip = null;
    }
    public void SetEndBGM(BGMSTATE bgm)
    {
        AudioClip clip = clipList[(int)bgm];
        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
