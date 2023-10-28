using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterDeath : MonoBehaviour
{
    public GameObject brokenRobot;
    public PlayerAnimationController animationController;
    Vector3 deathPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D water)
    {
        if (water.gameObject.tag.Equals("water") == true)
        {
            Animator animator;
            animator = animationController.DieAnimation();
            deathPosition = new Vector3(transform.position.x, transform.position.y, 0);
            Instantiate(brokenRobot, deathPosition, Quaternion.identity);
            Destroy(gameObject);

        }
    }
}   
