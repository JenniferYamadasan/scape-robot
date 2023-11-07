using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    public PlayerDestroyCounter destroycounter;
    public particleManager particlemanager;
    public GameObject playerModel;
    Vector3 startPosition;
    Vector3 deathPosition;
    public GameObject brokenRobot;
    public PlayerAnimationController animationController;
    public GameObject playerRobot;
    Quaternion deathRotation;
    [SerializeField] Transform model;


    [SerializeField] float yUp;

    [SerializeField] ItemCollider itemCollider;



    // Start is called before the first frame update
    void Start()
    {

        startPosition = new Vector3(model.transform.position.x, model.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Death()
    {
        animationController.StartIsDie();
    }

    public void PosSetthing(DIRECTION direction)
    {
        itemCollider.ItemReset();
        deathPosition = new Vector3(model.transform.position.x, model.transform.position.y+ yUp, 0);
        // deathRotation = transform.rotation;
        particlemanager.respawnParticle.Play();
        GameObject deadObj = Instantiate(brokenRobot, deathPosition, Quaternion.identity);
        if(deadObj.TryGetComponent<ThrowableObject>(out ThrowableObject throwableObject))
        {
            throwableObject.OnRotate(direction);
        }
        destroycounter.DestroyCounterAdd();
        gameObject.transform.position = startPosition;
        playerModel.transform.rotation = Quaternion.Euler(0, 140, 0);
    }

}
