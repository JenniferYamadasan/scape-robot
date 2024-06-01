using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeath : MonoBehaviour
{
    public DeathScript deathscript;
    public ParticleManager particlemanager;

    [SerializeField] private SEPlayer m_sePlayer = null;
    [SerializeField] private AudioClip m_seWater = null;
    [SerializeField] private AudioClip m_seExplosion = null;
    [SerializeField] private AudioClip m_seElectricShock = null;

    private void OnTriggerEnter2D(Collider2D hit)
    {
        //水に触れたら
        if (hit.gameObject.tag.Equals("water") == true)
        {
            IsDeadlyWaterDetected(m_seWater, particlemanager.electricParticle);
        }
        //地雷に触れたら
        if (hit.gameObject.tag.Equals("mine") == true)
        {
            IsDeadlyWaterDetected(m_seExplosion, particlemanager.explosionParticle);
            Destroy(hit.gameObject);
        }
    }

    /// <summary>
    /// 死んだ時の処理
    /// </summary>
    /// <param name="dethCilp"></param>
    /// <param name="effect"></param>
    void IsDeadlyWaterDetected(AudioClip dethCilp, ParticleSystem effect)
    {
        m_sePlayer.PlaySE(m_seElectricShock);
        m_sePlayer.PlaySE(dethCilp);
        deathscript.Death();
        effect.Play();
    }
}
