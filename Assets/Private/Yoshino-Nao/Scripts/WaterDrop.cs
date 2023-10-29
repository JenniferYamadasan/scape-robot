using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    /// <summary>スケール</summary>
    private　float m_scale = 0.5f;
    /// <summary>水滴の落下速度</summary>
    private float m_dropSpeed = 1.0f;

    private Transform m_tf = null;
    private Rigidbody2D m_rb = null;
    // Start is called before the first frame update
    void Start()
    { 
        m_rb = GetComponent<Rigidbody2D>();
        m_tf = GetComponent<Transform>();
        if(m_tf != null)
        {
            m_tf.localScale = new Vector3(m_scale, m_scale, m_scale);
        }
    }
    private void Update()
    {
        if (m_rb != null)
        {
            m_rb.AddForce(-Vector3.up * m_dropSpeed, ForceMode2D.Force);
        }
    }
    //初期化関数、水滴生成時に呼ばれる
    public void SetUp(float radius,float dropSpeed)
    {
        m_dropSpeed = dropSpeed;
        m_scale = radius;
        m_rb = GetComponent<Rigidbody2D>();
        if (m_tf != null)
        {
            m_tf.localScale = new Vector3(m_scale, m_scale, m_scale);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            //プレイヤーが当たった時の処理
            if (other.tag == "Player")
            {

            }
            if (other.GetComponent<WaterDrop>() == null)
            {
                Destroy(this.gameObject);
            }

        }

        Debug.Log(this.gameObject.name + "にHit!");
    }
}
