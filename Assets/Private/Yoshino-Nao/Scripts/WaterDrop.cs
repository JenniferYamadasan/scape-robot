using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    /// <summary>水滴の大きさ</summary>
    private float m_scale = 0.5f;
    /// <summary>水滴の落ちる速度</summary>
    private float m_dropSpeed = 1.0f;

    private Transform m_tf = null;
    private Rigidbody2D m_rb = null;

    void Start()
    { 
        m_rb = GetComponent<Rigidbody2D>();
        m_tf = GetComponent<Transform>();
        if(m_tf != null)
        {
            m_tf.localScale = new Vector3(m_scale, m_scale, m_scale);
        }
    }
    private void FixedUpdate()
    {
        if (m_rb != null)
        {
            m_rb.AddForce(-Vector3.up * m_dropSpeed, ForceMode2D.Force);
        }
    }

    //水滴生成時の初期化関数(WaterDropGeneraterが呼び出す)
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
            //当たったものが水滴でなければ削除
            if (other.GetComponent<WaterDrop>() == null)
            {
                //何かしらに触れたら消えるようにする
                Destroy(this.gameObject);
            }

        }
    }
}
