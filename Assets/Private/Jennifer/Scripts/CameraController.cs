using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // ResultCameraクラスのインスタンス
    private ResultCamera resultCamera;

    // 結果画面の位置
    [SerializeField] private Vector3 resultPos;

    // 結果UIのTransform
    [SerializeField] private Transform resultUITrans;

    // ミッションクリアUI
    [SerializeField] private GameObject missionClearUI;

    ///カメラの最大ポジション
    const int MAX_CAMERA_POSITON_Y = 34;

    void Start()
    {
        // ミッションクリアUIを非表示に設定
        missionClearUI.SetActive(false);

        // ResultCameraManagerからresultCameraを取得
        resultCamera = ResultCameraManager.resultCameraManager.resultCamera;

        // rendererの色のアルファ値を0に設定（透明にする）
        Color objectColor = resultCamera.renderer.material.color;
        objectColor.a = 0;
        resultCamera.renderer.material.color = objectColor;

        // UIの更新を開始
        StartCoroutine(UIUpdate());
    }

    /// <summary>
    /// カメラの移動を開始するメソッド
    /// </summary>
    public void StartCamera()
    {
        resultCamera.isMoveCamera = true;
        resultCamera.cameraTransform.DOMove(resultCamera.targetPosition, resultCamera.moveDuration);
        resultUITrans.DOLocalMove(resultPos, 6);
    }

    /// <summary>
    /// UIの更新を行うコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator UIUpdate()
    {
        while (true)
        {
            // カメラが移動中の場合
            if (resultCamera.isMoveCamera)
            {
                // カメラのY座標が34以上になった場合、ミッションクリアUI表示
                if (resultCamera.cameraTransform.position.y >= MAX_CAMERA_POSITON_Y)
                {
                    printError();
                    resultCamera.isMoveCamera = false;
                }
            }

            // canvasGroupのアルファ値が設定値以上になった場合、アイテムの評価を開始
            if (resultCamera.canvasGroup.alpha >= resultCamera.alpha)
            {
                StartCoroutine(resultCamera.playerDestroyCounter.AppraiseItem(resultCamera.scoreTime));
                yield break;
            }

            yield return null;
        }
    }

    /// <summary>
    /// ミッションクリアUI表示
    /// </summary>
    void printError()
    {
        resultCamera.canvasGroup.DOFade(1.0F, resultCamera.fadeInSpeed);
        missionClearUI.SetActive(true);
        resultCamera.renderer.material.DOFade(1.0F, resultCamera.fadeInSpeed);
    }
}