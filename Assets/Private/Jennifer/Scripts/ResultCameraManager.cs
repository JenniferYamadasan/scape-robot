using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

[Serializable]
public class ResultCamera
{
    // プレイヤーの破壊カウンター
    [field: SerializeField] public PlayerDestroyCounter playerDestroyCounter { get; private set; }

    // カメラの移動時間
    [field: SerializeField] public float moveDuration { get; private set; } = 2f;

    // CanvasGroup
    [field: SerializeField] public CanvasGroup canvasGroup { get; private set; }

    // カメラのTransform
    [field: SerializeField] public Transform cameraTransform { get; private set; }

    // カメラが移動中かどうかのフラグ
    [HideInInspector] public bool isMoveCamera;

    // Renderer
    [field: SerializeField] public Renderer renderer { get; private set; }

    // 移動の継続時間
    [field: SerializeField, Header("移動の継続時間")] public float DurationSeconds { get; private set; }

    // イージングタイプ
    [field: SerializeField, Header("イージングタイプ")] public Ease EaseType { get; private set; }

    // フェードインの速度
    [field: SerializeField, Header("フェードインの速度")] public float fadeInSpeed { get; private set; }

    // 移動先のターゲットポジション
    [field: SerializeField, Header("移動先のターゲットポジション")] public Vector3 targetPosition { get; private set; }

    // アルファ値
    [field: SerializeField, Header("アルファ値"), Min(0.7f)] public float alpha;

    // スコア表示時間
    [field: SerializeField, Header("スコア表示時間")] public float scoreTime { get; private set; }
}

public class ResultCameraManager : MonoBehaviour
{
    // 結果カメラマネージャのインスタンス
    public static ResultCameraManager resultCameraManager { get; private set; }

    // ResultCameraオブジェクト
    [field: SerializeField] public ResultCamera resultCamera { get; private set; }

    // ゲームのリンクを開くメソッド
    public void OnClick()
    {
        Application.OpenURL("https://game-ui.net/?p=835");
    }

    void Awake()
    {
        // シングルトンパターンの実装
        if (resultCameraManager == null)
        {
            resultCameraManager = this;
        }
    }
}