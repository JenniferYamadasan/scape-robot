using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    /// <summary>�ڕW�n�_���X�g</summary>
    [SerializeField] private List<GameObject> m_offsetPoses = new List<GameObject>();
    /// <summary>�ڕW�܂ňړ�����</summary>
    [SerializeField] private float m_moveTime = 3.0f;
    /// <summary>�o�ߎ���</summary>
    private float m_elapsedTime = 0f;
    ///// <summary>�v�f�ԍ�</summary>
    private int m_index = 1;
    /// <summary>�ڕW�n�_</summary>
    private Vector3 m_fromPos = Vector3.zero;
    /// <summary>�J�n�n�_</summary>
    private Vector3 m_toPos = Vector3.zero;
    private CircleCollider2D m_collider = null;
    private Rigidbody2D m_rb = null;
    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<CircleCollider2D>();
        m_rb = GetComponent<Rigidbody2D>();
        //���X�g��0�Ԗڂ������ʒu�ɐݒ�
        m_fromPos = m_offsetPoses[0].transform.position;
        //���W�������ʒu�ɏ�����
        transform.position = m_offsetPoses[0].transform.position;
        //�ڕW�n�_�����X�g�̐擪�̎��̍��W�ɏ�����
        m_toPos = m_offsetPoses[m_index].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //���X�g��0~1�A1~2�̂悤�ɓ����悤��
        if (m_rb != null)
        {
            //�C�[�W���O�֐��ŏ��X�Ɍ�������悤�ɍ��W���Ԃ���
            float alpha = CubicOut(m_elapsedTime, m_moveTime, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(m_fromPos, m_toPos, alpha);
            //�o�ߎ��Ԃ��ݒ肵�����Ԃ𒴂�����
            if (alpha > 1f)
            {
                //�o�ߎ��Ԃ����Z�b�g���AtoPos��fromPos�������ĕ�Ԃ��J�n
                m_elapsedTime = 0f;
                transform.position = m_fromPos = m_toPos;
                m_index++;
                //���X�g�̖����̏ꍇ�͂܂��擪����X�^�[�g
                if (m_offsetPoses.Count <= m_index)
                {
                    m_index = 0;
                }
                m_toPos = m_offsetPoses[m_index].transform.position;
            }
        }
        //�o�ߎ��Ԃ��v��
        m_elapsedTime += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            //�v���C���[�������������̏���
            if (other.tag == "Player")
            {

            }


        }
        Debug.Log(this.gameObject.name + "��Hit!");

    }
    public static float CubicOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * (t * t * t + 1) + min;
    }
}
