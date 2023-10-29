using UnityEngine;

public class WaterDropGenerater : MonoBehaviour
{
    /// <summary>�����鐅�H�I�u�W�F�N�g</summary>
    [SerializeField] private GameObject m_waterObj = null;
    /// <summary>���H�̐����ʒu�̕␳�l</summary>
    [SerializeField] private Vector3 m_offsetPos = Vector3.zero;
    /// <summary>���H�̐����p�x</summary>
    [SerializeField] private float m_dropRate = 1.0f;
    /// <summary>���H�̓����蔻��̔��a</summary>
    [SerializeField] private float m_waterRadius = 0.5f;
    /// <summary>���H�̗������x</summary>
    [SerializeField] private float m_dropSpeed = 1.0f;

    /// <summary>��������Ă���̌o�ߎ���</summary>
    private float m_elapsedTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //�o�ߎ��Ԃ����𒴂���Ɛ��H�𐶐�
        if (m_elapsedTime >= m_dropRate)
        {
            GameObject WaterObj = Instantiate(m_waterObj, transform.position + m_offsetPos, Quaternion.identity);
            WaterDrop WaterDrop = WaterObj.GetComponent<WaterDrop>();
            //���H�̗������x��ݒ�
            WaterDrop.SetUp(m_waterRadius, m_dropSpeed);
            //�����Ɠ����Ɍo�ߎ��Ԃ����Z�b�g
            m_elapsedTime = 0f;
        }
        //�o�ߎ��Ԃ̌v��
        m_elapsedTime += Time.deltaTime;
    }
}
