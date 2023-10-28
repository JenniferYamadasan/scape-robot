using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    Vector3 startPosition;
    Vector3 deathPosition;
    public GameObject brokenRobot;
    public PlayerAnimationController animationController;
    public GameObject playerRobot;
 
    // Start is called before the first frame update
    void Start()
    {
        
        startPosition= new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Death()
    {
        
        
        Animator animator;
        animator = animationController.DieAnimation();
        deathPosition = new Vector3(transform.position.x, transform.position.y, 0);
        Instantiate(brokenRobot, deathPosition, Quaternion.identity);
        gameObject.transform.position = startPosition;



    }

}
