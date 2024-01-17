using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject bgmManagerObject;
    public static GameManager gameManager { get; private set; }

    public static BGMManager bgmManager { get; private set; }

    void Awake()
    {
        #region SingletonçÏê¨
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        else { Destroy(this.gameObject); }

        if (bgmManager == null)
        {
            GameObject bgmObject = Instantiate(bgmManagerObject);
            if(bgmManagerObject.TryGetComponent(out BGMManager bgmSc))
            {
                bgmManager = bgmSc;
            }
            DontDestroyOnLoad (bgmObject);
        }
        else { Destroy(this.gameObject); }
        #endregion
    }
}
