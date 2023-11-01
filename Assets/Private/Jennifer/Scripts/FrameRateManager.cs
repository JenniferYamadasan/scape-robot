using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    [SerializeField,Range(30,120)] int frameRate;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = frameRate;   //60fpsに固定
    }
}
