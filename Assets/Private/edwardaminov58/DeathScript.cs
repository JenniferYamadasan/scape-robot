using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    PlayerDestroyCounter destroycounter;
    public ParticleManager particlemanager;
    public GameObject playerModel;
    Vector3 startPosition;
    Vector3 deathPosition;
    public GameObject brokenRobot;
    public PlayerAnimationController animationController;
    public GameObject playerRobot;
    [SerializeField] Transform model;


    [SerializeField] float yUp;

    [SerializeField] ItemCollider itemCollider;

    [SerializeField] PlayerController playerController;



    // Start is called before the first frame update
    void Start()
    {
        destroycounter = FindObjectOfType<PlayerDestroyCounter>();
        startPosition = new Vector3(model.transform.position.x, model.transform.position.y, 0);
    }
    //死んだ際にアニメーション再生
    public void Death()
    {
        animationController.StartIsDie();
    }

    /// <summary>
    /// 死体を生成
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="isFold"></param>
    public void PosSetthing(DIRECTION direction ,bool isFold)
    {
        //持っているアイテムをリセットする
        itemCollider.ItemReset();
        //死体の位置を自身の現在の位置にするY軸をそのままにするとバグるため少しあげてる
        deathPosition = new Vector3(model.transform.position.x, model.transform.position.y+ yUp, 0);
        //リスポーンアニメーション再生
        particlemanager.respawnParticle.Play();

        //アイテムを持っていなかったらそのまま死亡したオブジェクト生成
        //その後角度調整
        if(!isFold)
        {
            GameObject deadObj = Instantiate(brokenRobot, deathPosition, Quaternion.identity);
            if (deadObj.TryGetComponent(out ThrowableObject throwableObject))
            {
                throwableObject.OnRotate(direction);
            }
        }
        //死亡した数をカウントする
        destroycounter.DestroyCounterAdd();

        //ポジションをスポーン位置に戻す
        gameObject.transform.position = startPosition;
        playerModel.transform.rotation = Quaternion.Euler(0, 140, 0);
    }

}
