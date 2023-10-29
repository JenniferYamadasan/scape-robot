using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource = null;


    public void PlaySE(AudioClip se)
    {
        if (m_audioSource != null)
        {
            m_audioSource.PlayOneShot(se);
        }
    }
}
