using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneSubSystem
{
    public static int GetStageIndex()
    {
        // 現在のシーンのパスを取得
        string currentScenePath = SceneManager.GetActiveScene().path;

        // ビルド設定に登録されている全てのシーンのパスを取得
        string[] scenePaths = new string[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            scenePaths[i] = SceneUtility.GetScenePathByBuildIndex(i);
        }
        // 現在のシーンがビルド設定の何番目に登録されているか調べる
        return System.Array.IndexOf(scenePaths, currentScenePath);
    }
}
