using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BGMSTATE
{
    TITLE=0,
    ENDING=1,
}
public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject bgmManagerObject;
    public static GameManager gameManager { get; private set; }

    public static BGMManager bgmManager { get; private set; }

    static FrameRate  frameInstance;

    void Awake()
    {
        #region SingletonçÏê¨
        if (gameManager == null)
        {
            gameManager = this;
            frameInstance =  new FrameRate();
            DontDestroyOnLoad(this);
        }
        else { Destroy(this.gameObject); }

        if (bgmManager == null)
        {
            GameObject bgmObject = Instantiate(bgmManagerObject);
            if (bgmObject.TryGetComponent(out BGMManager bgmSc))
            {
                bgmManager = bgmSc;
            }
            else
            {
                bgmManager = bgmObject.gameObject.AddComponent<BGMManager>();
            }
            DontDestroyOnLoad (bgmObject);
        }
        else 
        {
            ActiveScene();
            Destroy(this.gameObject); 
        }
        #endregion
    }

    void Start()
    {
        ActiveScene();
    }
    void ActiveScene()
    {
        if (SceneManager.GetActiveScene().name == "result")
        {
            bgmManager.SetEndBGM(BGMSTATE.ENDING);
        }
        else
        {
            bgmManager.SetEndBGM(BGMSTATE.TITLE);
        }
    }
}

public class FrameRate
{
    public FrameRate()
    {
        Application.targetFrameRate = 120;   //60fpsÇ…å≈íË
    }
}