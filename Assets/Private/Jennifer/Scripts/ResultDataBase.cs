using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;


[Serializable]
[CreateAssetMenu(fileName ="ResultCamera",menuName ="リザルトシーン/エンディング調整用のデータベース")]
public class ResultDataBase : ScriptableObject
{
    [field: SerializeField] public Vector3 targetPosition { get; private set; }
    [field: SerializeField] public float moveDuration { get; private set; } = 2f;

    [field: SerializeField] public Transform cameraTransform { get; private set; }

    [field: SerializeField] public float DurationSeconds { get; private set; }
    [field: SerializeField] public Ease EaseType { get; private set; }

    [field: SerializeField] public CanvasGroup canvasGroup { get; private set; }
    [field: SerializeField] public Text attentionText { get; private set; }

    [field: SerializeField] public int loopNum { get; private set; }

    public bool isMoveCamera;

    [field: SerializeField] public float fadeInSpeed { get; private set; }

    [field: SerializeField] public PlayerDestroyCounter playerDestroyCounter { get; private set; }

    [field: SerializeField] public float alpha;
}
