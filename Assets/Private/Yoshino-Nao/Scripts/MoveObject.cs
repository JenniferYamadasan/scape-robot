using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MoveObject : MonoBehaviour
{
    /// <summary>‰Ÿ‚µ‚Ä‚¢‚é‚©‚Ìƒtƒ‰ƒO</summary>
    [SerializeField] private bool m_isInteract = false;
    [SerializeField] private UnityEvent m_event = new UnityEvent();

    private Vector3 m_playerPos = Vector3.zero;
    private Rigidbody m_rb = null;
    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isInteract)
        {

        }
        else
        {

        }
    }

    public void InteractEnabled()
    {
        m_rb.isKinematic = true;
        m_isInteract = true;
    }
    public void IntaractDisabled()
    {
        m_rb.isKinematic = false;
        m_isInteract = false;
    }
}
