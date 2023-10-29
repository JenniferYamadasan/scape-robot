using System.Collections.Generic;
using UnityEngine;

public class WalkSEPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> m_clips = new List<AudioClip>();
    [SerializeField] private float m_pitchRange = 0.1f;
    [SerializeField] private AudioSource m_audioSource = null;


    public void PlayWalkSE()
    {
        m_audioSource.pitch = 1.0f + Random.Range(-m_pitchRange, m_pitchRange);
        m_audioSource.PlayOneShot(m_clips[Random.Range(0, m_clips.Count)]);
    }
}
