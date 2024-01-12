using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoldCollision : MonoBehaviour
{
    //©g‚ª°•”G‚ê‚Ä‚¢‚é‚©‚Ç‚¤‚©
    public bool isCollsion { get; private set; } =false;


    //°‚ÉG‚ê‚½‚çisCollsion‚ğtrue‚É‚·‚é
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == 8)
        {
            isCollsion = true;
        }
    }

    //°‚É—£‚ê‚½‚çisCollsion‚ğfalse‚É‚·‚é
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.layer == 8)
        {
            isCollsion = false;
        }
    }
}
