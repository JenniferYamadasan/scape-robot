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

    //�v����
    [field: SerializeField, Header("���b��ɂŃJ�����̈ړ����������Ă��邩")] public float DurationSeconds { get; private set; }
    [field: SerializeField, Header("�ǂ�Ȋ֐����B�����o���Ȃ��̂ŉ��ɂ���{�^������������T�C�g�ɂƂт܂��B")] public Ease EaseType { get; private set; }
    [field:SerializeField] public Renderer renderer { get; private set; }
    [field: SerializeField, Header("���[�v�����")] public int loopNum { get; private set; }
    [field: SerializeField, Header("�t�F�[�h�C������X�s�[�h")] public float fadeInSpeed { get; private set; }
    [field: SerializeField,Header("�ړ���̍ŏI�I�̃J�����̃|�W�V����")] public Vector3 targetPosition { get; private set; }

    [field: SerializeField, Header("�A���t�@�l���ǂ̂��炢�̎�����e�L�X�g�����X�Ƀt�F�[�h�C�����邩") , Min(0.7f)] public float alpha;
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
