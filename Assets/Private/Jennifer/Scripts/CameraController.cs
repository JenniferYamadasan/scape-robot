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
    public void StartCamera()
    {
        isMoveCamera = true;
        cameraTransform.transform.DOMove(targetPosition, moveDuration);
    }

    void Update()
    {
        if(isMoveCamera)
        {
            if(cameraTransform.position.y >=31)
            {
                printError("mission complete");
                isMoveCamera=false;
            }
        }
    }


    /// <summary>
    /// エラー内容を出力
    /// </summary>
    /// <param name="error"></param>
    void printError(string error)
    {
        attentionText.text = error;
        canvasGroup.DOFade(1.0F, fadeInSpeed);
    }
}
