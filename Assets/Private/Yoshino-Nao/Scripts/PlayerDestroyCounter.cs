using TMPro;
using UnityEngine;
using System.Collections.Generic;
public class PlayerDestroyCounter : MonoBehaviour
{
    /// <summary>�v���C���[���S�J�E���g</summary>
    [SerializeField] private static int m_destroyCounter = 0;

    [SerializeField] List<MeshFilter> counter = new List<MeshFilter>();
    
    [SerializeField] List<Mesh> meshNumber = new List<Mesh>();
    private void Start()
    {
        UIUpdate();
    }
    /// <summary>���S�J�E���g�����Z</summary>
    public void DestroyCounterAdd()
    {
        m_destroyCounter++;
        UIUpdate();
    }

    void UIUpdate()
    {
        int hundredsPlace = m_destroyCounter / 100;
        int tensPlace = (m_destroyCounter - (hundredsPlace * 100)) / 10;
        int unitsPlace = (m_destroyCounter - (hundredsPlace * 100 + tensPlace * 10));

        //Debug.Log($"hundredsPlace = {hundredsPlace} tensPlace = {tensPlace} unitsPlace = {unitsPlace}");
        counter[0].mesh = meshNumber[unitsPlace];
        counter[1].mesh = meshNumber[tensPlace];
        counter[2].mesh = meshNumber[hundredsPlace];
    }
    /// <summary>���S�J�E���g�����Z�b�g</summary>
    /// <param name="value">�������l</param>
    public void DestroyCounterReset(int value)
    {
        m_destroyCounter = value;
    }

    /// <summary>
    /// ���S��������Ԃ����\�b�h
    /// </summary>
    /// <returns></returns>
    public int GetDeathCount() { return m_destroyCounter; }

    /// <summary>
    /// ���߂����蒼���ۂɃ��Z�b�g����B
    /// </summary>
    public void Reset() { m_destroyCounter = 0; }
}
