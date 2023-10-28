using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    /// <summary>当たり判定の半径</summary>
    private　float m_radius = 0.5f;
    /// <summary>水滴の落下速度</summary>
    private float m_dropSpeed = 1.0f;


    private Rigidbody m_rb = null;
    private SphereCollider m_collider = null;
    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<SphereCollider>();
        m_rb = GetComponent<Rigidbody>();
        if (m_collider != null)
        {
            m_collider.radius = m_radius;
        }
        if(m_rb != null)
        {
            m_rb.useGravity = false;
        }
    }
    private void Update()
    {
        if (m_rb != null)
        {
            m_rb.AddForce(-Vector3.up * m_dropSpeed, ForceMode.Acceleration);
        }
    }
    //初期化関数、水滴生成時に呼ばれる
    public void SetUp(float radius,float dropSpeed)
    {
        m_dropSpeed = dropSpeed;
        m_radius = radius;
        m_collider = GetComponent<SphereCollider>();
        m_rb = GetComponent<Rigidbody>();
        if (m_collider != null)
        {
            m_collider.radius = m_radius;
        }
        if(m_rb != null)
        {
            m_rb.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            //プレイヤーが当たった時の処理
            if (other.tag == "Player")
            {

            }


        }

        
    }
}
