using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldCollision : MonoBehaviour
{
    public bool isCollsion { get; private set; } =false;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == 8)
        {
            isCollsion = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == 8)
        {
            isCollsion = false;
        }
    }
}
