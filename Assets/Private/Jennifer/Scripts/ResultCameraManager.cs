using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

[Serializable]
public class ResultCamera
{

    [field: SerializeField] public PlayerDestroyCounter playerDestroyCounter { get; private set; }
    [field: SerializeField] public float moveDuration { get; private set; } = 2f;
    [field: SerializeField] public CanvasGroup canvasGroup { get; private set; }
    [field: SerializeField] public Transform cameraTransform { get; private set; }

    [HideInInspector] public bool isMoveCamera;

    //要調整
    [field: SerializeField, Header("何秒後にでカメラの移動を完了しているか")] public float DurationSeconds { get; private set; }
    [field: SerializeField, Header("どんな関数か。説明出来ないので下にあるボタンを押したらサイトにとびます。")] public Ease EaseType { get; private set; }
    [field:SerializeField] public Renderer renderer { get; private set; }
    [field: SerializeField, Header("ループする回数")] public int loopNum { get; private set; }
    [field: SerializeField, Header("フェードインするスピード")] public float fadeInSpeed { get; private set; }
    [field: SerializeField,Header("移動後の最終的のカメラのポジション")] public Vector3 targetPosition { get; private set; }

    [field: SerializeField, Header("アルファ値がどのぐらいの時からテキストを徐々にフェードインするか") , Min(0.7f)] public float alpha;
}
public class ResultCameraManager : MonoBehaviour
{  
    public void OnClick()
    {
        Application.OpenURL("https://game-ui.net/?p=835");
    }
    public static ResultCameraManager resultCameraManager { get; private set; }

    [field:SerializeField] public ResultCamera resultCamera { get; private set; }

    void Start()
    {
        if(resultCameraManager == null)
        {
            resultCameraManager = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
