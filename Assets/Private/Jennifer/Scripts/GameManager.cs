using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    internal static BGMManager bgmManager;

    void Awake()
    {
        if (gameManager == null) gameManager = this;
        else { Destroy(this.gameObject); }
    }
}
