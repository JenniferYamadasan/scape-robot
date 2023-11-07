using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDeath : MonoBehaviour
{
    public DeathScript deathscript;
    public particleManager particlemanager;
    [SerializeField] private SEPlayer m_sePlayer = null;
    [SerializeField] private AudioClip m_seWater = null;
    [SerializeField] private AudioClip m_seExplosion = null;
    [SerializeField] private AudioClip m_seElectricShock = null;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.gameObject.tag.Equals("water") == true)
        {
            m_sePlayer.PlaySE(m_seElectricShock);
            m_sePlayer.PlaySE(m_seWater);
            deathscript.Death();
            //Debug.Log("WATER");
            particlemanager.electricParticle.Play();
        }
        if (hit.gameObject.tag.Equals("mine") == true)
        {
            m_sePlayer.PlaySE(m_seElectricShock);
            m_sePlayer.PlaySE(m_seExplosion);
            deathscript.Death();
            //Debug.Log("MINE");
            Destroy(hit.gameObject);
            particlemanager.explosionParticle.Play();
        }
    }
    }
