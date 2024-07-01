using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoom : MonoBehaviour
{
    public InputField roomNameInput;
    public NetworkManager networkManager;

    public void OnJoinRoomButtonClicked()
    {
        string roomName = roomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            networkManager.JoinRoom(roomName);
        }
        else
        {
            Debug.LogWarning("Room name is empty!");
        }
    }
}
