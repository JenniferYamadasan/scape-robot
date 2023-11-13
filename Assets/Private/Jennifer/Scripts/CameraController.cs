using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    ResultCamera resultCamera;

    void Start()
    {
        resultCamera = ResultCameraManager.resultCameraManager.resultCamera;

        resultCamera.playerDestroyCounter.Reset();

        Color objectColor = resultCamera.renderer.material.color;
        objectColor.a = 0;
        resultCamera.renderer.material.color = objectColor;


        StartCoroutine(UIUpdate());
    }
    public void StartCamera()
    {
        resultCamera.isMoveCamera = true;
        resultCamera.cameraTransform.transform.DOMove(resultCamera.targetPosition, resultCamera.moveDuration);
    }


    IEnumerator UIUpdate()
    {
        while(true)
        {
            if (resultCamera.isMoveCamera)
            {
                if (resultCamera.cameraTransform.position.y >= 34)
                {
                    printError();
                    resultCamera.isMoveCamera = false;
                }
            }

            if (resultCamera.canvasGroup.alpha >=resultCamera.alpha)
            {
                StartCoroutine(resultCamera.playerDestroyCounter.AppraiseItem());
                yield break;
            }

            yield return null;
        }
    }


    /// <summary>
    /// エラー内容を出力
    /// </summary>
    /// <param name="error"></param>
    void printError()
    {
        resultCamera.canvasGroup.DOFade(1.0F, resultCamera.fadeInSpeed);
        resultCamera.renderer.material.DOFade(1.0F, resultCamera.fadeInSpeed);
    }
}
