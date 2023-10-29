using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCall : MonoBehaviour
{
    [SerializeField] private SEPlayer m_se = null;
    [SerializeField]private AudioClip m_clip = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            m_se.PlaySE(m_clip);
        }
    }
}
