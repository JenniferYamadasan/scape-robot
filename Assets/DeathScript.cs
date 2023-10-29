using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public GameObject playerModel;
    Vector3 startPosition;
    Vector3 deathPosition;
    public GameObject brokenRobot;
    public PlayerAnimationController animationController;
    public GameObject playerRobot;
    public float yPosUp;
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
        animationController.StartIsDie();
    }

    public void PosSetthing()
    {
        deathPosition = new Vector3(transform.position.x, transform.position.y+ yPosUp, 0);
        Instantiate(brokenRobot, deathPosition, Quaternion.identity);
        gameObject.transform.position = startPosition;
        playerModel.transform.rotation = Quaternion.Euler(0, 140, 0);
    }

}
