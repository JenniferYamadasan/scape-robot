using UnityEngine;

public class WaterDrop : MonoBehaviour
{
    /// <summary>�X�P�[��</summary>
    private float m_scale = 0.5f;
    /// <summary>���H�̗������x</summary>
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
    //�������֐��A���H�������ɌĂ΂��
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
            //�v���C���[�������������̏���
            if (other.tag == "Player")
            {

            }
            if (other.GetComponent<WaterDrop>() == null)
            {
                Destroy(this.gameObject);
            }

        }

        Debug.Log(this.gameObject.name + "��Hit!");
    }
}
