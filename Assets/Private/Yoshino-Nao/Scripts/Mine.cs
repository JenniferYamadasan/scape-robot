using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    /// <summary>目標地点リスト</summary>
    [SerializeField] private List<GameObject> m_offsetPoses = new List<GameObject>();
    /// <summary>目標まで移動する</summary>
    [SerializeField] private float m_moveTime = 3.0f;
    /// <summary>経過時間</summary>
    private float m_elapsedTime = 0f;
    ///// <summary>要素番号</summary>
    private int m_index = 1;
    /// <summary>目標地点</summary>
    private Vector3 m_fromPos = Vector3.zero;
    /// <summary>開始地点</summary>
    private Vector3 m_toPos = Vector3.zero;
    private CircleCollider2D m_collider = null;
    private Rigidbody2D m_rb = null;
    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<CircleCollider2D>();
        m_rb = GetComponent<Rigidbody2D>();
        //リストの0番目を初期位置に設定
        m_fromPos = m_offsetPoses[0].transform.position;
        //座標を初期位置に初期化
        transform.position = m_offsetPoses[0].transform.position;
        //目標地点をリストの先頭の次の座標に初期化
        m_toPos = m_offsetPoses[m_index].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //リストの0~1、1~2のように動くように
        if (m_rb != null)
        {
            //イージング関数で徐々に減速するように座標を補間する
            float alpha = CubicOut(m_elapsedTime, m_moveTime, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(m_fromPos, m_toPos, alpha);
            //経過時間が設定した時間を超えたら
            if (alpha > 1f)
            {
                //経過時間をリセットし、toPosにfromPosを代入して補間を開始
                m_elapsedTime = 0f;
                transform.position = m_fromPos = m_toPos;
                m_index++;
                //リストの末尾の場合はまた先頭からスタート
                if (m_offsetPoses.Count <= m_index)
                {
                    m_index = 0;
                }
                m_toPos = m_offsetPoses[m_index].transform.position;
            }
        }
        //経過時間を計測
        m_elapsedTime += Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            //プレイヤーが当たった時の処理
            if (other.tag == "Player")
            {

            }


        }
        Debug.Log(this.gameObject.name + "にHit!");

    }
    public static float CubicOut(float t, float totaltime, float min, float max)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * (t * t * t + 1) + min;
    }
}
