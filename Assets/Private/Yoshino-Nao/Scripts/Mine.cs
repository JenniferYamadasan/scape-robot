using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    //当たり判定の半径
    [SerializeField] private float m_radius = 1f;

    private SphereCollider m_collider = null;
    private Rigidbody m_rb = null;
    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<SphereCollider>();
        m_rb = GetComponent<Rigidbody>();
        if(m_collider != null)
        {
            m_collider.radius = m_radius;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Debug.Log(this.gameObject.name + "にHit!");
    }
}
