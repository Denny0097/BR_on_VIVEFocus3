using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LeftEyeDisplay : MonoBehaviour
{
    public VideoPlayer _video;
    public DisplayControl _displayControl;

    void Update()
    {
        // 收到開始訊號後開始右眼實驗畫面的顯示
        if (_displayControl._roundStart)
        {
            _video.Play();
        }

        // 收到結束訊號後停止右眼實驗畫面的顯示
        if (_displayControl._roundEnd)
        {
            _video.Pause();
        }
    }
}
