//アニメーションの管理をしている


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerAnimationController : MonoBehaviour
{

    /// <summary>
    /// PlayerのAnimatorを格納している
    /// </summary>
    [field:SerializeField] public Animator animator { get; private set; }

    /// <summary>
    /// 現在持っているアイテムを保持しているクラス
    /// </summary>
    [SerializeField] PlayerHaveItem playerHaveItem;

    /// <summary>
    /// 持つアイテムを決めているスクリプト
    /// </summary>
    [SerializeField] ItemCollider itemCollider;

    /// <summary>
    /// 入力値を管理している
    /// </summary>
    [SerializeField] InputManager inputManager;

    /// <summary>
    /// 死ぬ際に使うクラス
    /// </summary>
    [SerializeField] DeathScript deathScript;

    [SerializeField] PlayerInput playerInput;

    /// アニメーションのパラメータを変数に入れている
    const string WALK_ANIMATION = "isWalk";
    const string ITEM_HAVE_ANIMATION= "hasItem";
    const string DIE_ANIMATION = "isDie";
    const string THROW_ANIMATION = "isThrow";
    const string ISPUSH_ANIMATION = "isPush";
    const string SPEED_ANIMATION = "Speed";

    //BoxCollider2D itemCollider2D;
    WaitForSeconds wait;

    [SerializeField] float leftRotate;
    [SerializeField] float RightRotate;

    [SerializeField,Header("死んでからこの秒数待ってリスポーン")] float timeToWaitAfterDeathAnimation;
    void Start()
    {
        wait = new WaitForSeconds(timeToWaitAfterDeathAnimation);
        animator.SetFloat(SPEED_ANIMATION, -1);
        animator.Play("Spawn");
    }

    /// <summary>
    /// 歩くアニメーションを管理するメソッド
    /// </summary>
    /// <param name="result"></param>
    public void walkAnimator(bool result)
    {
        animator.SetBool(WALK_ANIMATION, result);
    }

    /// <summary>
    /// アイテムを持つアニメーションを流すメソッド
    /// </summary>
    /// <param name="result"></param>
    public void pickUpItem(DIRECTION direction)
    {
        //this.itemCollider2D = itemCollider2D;
        animator.SetBool(ITEM_HAVE_ANIMATION, AcquireItem(direction));
    }

    /// <summary>
    /// 持つアイテムが決まった際に呼ばれるメソッド
    /// </summary>
    public bool AcquireItem(DIRECTION direction)
    {
        //持つオブジェクトが決まったため、
        //ここで持つオブジェクトを決めているコライダーを無効化する
        //itemCollider2D.enabled = false;
        //持つアイテムを取得
        GameObject item = itemCollider.GetNearestObject();
        if (item == null) return false;

        //投げるアニメーションをtrueにする。
        //理由 投げるモーションからIdleになる条件がTrueになる事そのことを踏まえると
        //投げてからtrueにするとワンテンポ遅れる為、投げることが確定している今事前にfalseにしている
        animator.SetBool(THROW_ANIMATION, true);

        //アイテムの情報をクラスに引き渡す。
        playerHaveItem.hasItem = item;

        //プレイヤーのモデルの手元に配置したゲームオブジェクトの子オブジェクトに設定した後ポジションをコピーする。
        //アニメーションに合わせて移動するようになる
        playerHaveItem.hasItem.transform.parent = playerHaveItem.itemPos;
        item.transform.position = playerHaveItem.itemPos.position;


        //物理挙動無視、持っている間の当たり判定、飛ばす処理をするのに必要なスクリプトを取得する
        item.gameObject.TryGetComponent<BoxCollider2D>(out playerHaveItem.itemsCollider2D);
        item.gameObject.TryGetComponent<Rigidbody2D>(out playerHaveItem.itemRB2D);
        item.gameObject.TryGetComponent<ThrowableObject>(out playerHaveItem.throwableObject);

        //当たり判定をisTriggerにして貫通するようにする。
        playerHaveItem.itemsCollider2D.isTrigger = true;
        //isKinematicをtrueにして重力を無視している
        playerHaveItem.itemRB2D.isKinematic = true;
        //投げることが確定している為、ここでステートの変更
        inputManager.ChangeState(ITEMACTION.THROW);
        return true;
    }

    /// <summary>
    /// アイテムを投げるアニメーションを流すメソッド
    /// </summary>
    /// <param name="result"></param>
    public void Throw(DIRECTION direction)
    {
        //投げるモーションが流れた為、アイテムは持っていない為falseにしている
        animator.SetBool(ITEM_HAVE_ANIMATION, false);
        //持つ前の状態に戻して、アイテムを投げている。
        playerHaveItem.hasItem.transform.parent = null;
        playerHaveItem.itemsCollider2D.isTrigger = false;
        playerHaveItem.itemRB2D.isKinematic = false;
        playerHaveItem.throwableObject.OnThrow(direction);
        playerHaveItem.hasItem = null;
        playerHaveItem.itemRB2D = null;
        playerHaveItem.itemsCollider2D = null;
        playerHaveItem.throwableObject = null;

        //バグ懸念でfalseにしている。別になくても問題はない（と思う。）
        animator.SetBool(THROW_ANIMATION, false);
        //投げることが出来た為ステートの変更
        inputManager.ChangeState(ITEMACTION.HOLD);
    }

    /// <summary>
    /// アニメーションが流れ終わった際に呼ばれる
    /// </summary>
    public void EndDieAnimation()
    {
        StartCoroutine(die());
    }

    public void OnRotate(DIRECTION direction)
    {
        if (playerHaveItem.hasItem == null) return;
        if(direction == DIRECTION.RIGHT)
        {
            playerHaveItem.hasItem.transform.rotation = Quaternion.Euler(0,RightRotate,0);
        }
        else
        {
            playerHaveItem.hasItem.transform.rotation = Quaternion.Euler(0, leftRotate, 0);
        }
    }
    IEnumerator die()
    {
        yield return wait;
        //移動出来きるようにする。
        playerInput.enabled = true;
        //アイテムを持ったまま死ぬとバグの原因になるため、回避するようにしている。
        if (playerHaveItem.hasItem != null)
        {
            playerHaveItem.hasItem.transform.parent = null;
            playerHaveItem.hasItem.transform.position = playerHaveItem.throwableObject.pos;
            playerHaveItem.hasItem = null;
            playerHaveItem.throwableObject = null;
            inputManager.ChangeState(ITEMACTION.HOLD);
        }
        deathScript.PosSetthing();
        animator.Play("Spawn");
    }
    /// <summary>
    /// 死んだ瞬間に流すアニメーション
    /// </summary>
    public void StartDieAnimation()
    {
        //全てのアニメーションをリセットする
        AllAnimationReset();
        //死ぬアニメーションはAnyStateで行なっているため、すぐにfalseにする必要がある。
        animator.SetBool(DIE_ANIMATION, false);
        //移動出来ないようにする
        playerInput.enabled = false;


        //animator.SetBool(DIE_ANIMATION, false);
    }


    public void Goal()
    {
        animator.Play("Goal");
        StartCoroutine(test());
    }
    IEnumerator test()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(InputManager.goalNum);
    }
    /// <summary>
    /// 死ぬアニメーションを流す
    /// </summary>
    public void StartIsDie()
    {
        animator.SetBool(DIE_ANIMATION, true);
        //移動の入力値とVelocityの値を初期化
        inputManager.IsMoveFinish();
    }
    /// <summary>
    /// 死んだ際Die以外ののパラメータをリセットする
    /// </summary>
    public void AllAnimationReset()
    {
        animator.SetBool(WALK_ANIMATION, false);
        animator.SetBool(ITEM_HAVE_ANIMATION, false);
        animator.SetBool(THROW_ANIMATION, false);
        animator.SetBool(DIE_ANIMATION, false);
    }
}
