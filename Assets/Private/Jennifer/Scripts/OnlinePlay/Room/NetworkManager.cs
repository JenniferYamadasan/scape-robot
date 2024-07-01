using Fusion;
using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public NetworkRunner networkRunnerPrefab;
    private NetworkRunner networkRunner;

    private void Awake()
    {
        networkRunner = Instantiate(networkRunnerPrefab);
    }

    // ルームを作成するメソッド
    public async void CreateRoom(string roomName)
    {
        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Host,
            SessionName = roomName,
            Scene = SceneRef.FromIndex(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex),
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };

        await networkRunner.StartGame(startGameArgs);
    }

    // ルームに参加するメソッド
    public async void JoinRoom(string roomName)
    {
        var startGameArgs = new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SessionName = roomName,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        };

        await networkRunner.StartGame(startGameArgs);
    }
}
