#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(ResultCameraManager))]
public class ResultCameraComponent : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ResultCameraManager resultCameraManager = (ResultCameraManager)target;
        if (GUILayout.Button("Easy typeについてURLに飛ぶ"))
        {
            resultCameraManager.OnClick();
        }

    }


}
#endif