using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    [SerializeField]  Vector3 targetPosition;
    [SerializeField]  float moveDuration = 2f;

    [SerializeField] Transform cameraTransform;

    [SerializeField] float DurationSeconds;
    [SerializeField] Ease EaseType;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Text attentionText;

    [SerializeField] int loopNum;

    bool isMoveCamera;

    [SerializeField] float fadeInSpeed;

    [SerializeField] PlayerDestroyCounter playerDestroyCounter;

    void Start()
    {
        playerDestroyCounter.Reset();
        StartCoroutine(UIUpdate());
    }
    public void StartCamera()
    {
        isMoveCamera = true;
        cameraTransform.transform.DOMove(targetPosition, moveDuration);
    }


    IEnumerator UIUpdate()
    {
        while(true)
        {
            if (isMoveCamera)
            {
                if (cameraTransform.position.y >= 31)
                {
                    printError();
                    isMoveCamera = false;
                }
            }

            if (canvasGroup.alpha >=0.7f)
            {
                StartCoroutine(playerDestroyCounter.AppraiseItem());
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
        canvasGroup.DOFade(1.0F, fadeInSpeed);
    }
}
