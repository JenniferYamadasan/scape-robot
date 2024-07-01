using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    [SerializeField] InputField roomNameInput;
    [SerializeField] NetworkManager networkManager;

    public void OnCreateRoomButtonClicked()
    {
        string roomName = roomNameInput.text;
        if (!string.IsNullOrEmpty(roomName))
        {
            networkManager.CreateRoom(roomName);
        }
        else
        {
            Debug.LogWarning("Room name is empty!");
        }
    }
}
