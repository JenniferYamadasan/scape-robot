using UnityEngine;

public class SEPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource = null;
    [SerializeField] private PlayerController playerController;
    public void PlaySE(AudioClip se)
    {

        if (m_audioSource == null || playerController.isDie) return;
        m_audioSource.PlayOneShot(se);
    }
}
