using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDeath : MonoBehaviour
{
    public DeathScript deathscript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D hit)
    {

        if (hit.gameObject.tag.Equals("water") == true)
        {
            deathscript.Death();
            Debug.Log("WATER");
        }
        if (hit.gameObject.tag.Equals("mine") == true)
        {
            deathscript.Death();
            Debug.Log("MINE");
            Destroy(hit.gameObject);
        }
    }
}   
