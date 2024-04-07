using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LeftEyeDisplay : MonoBehaviour
{
   
    public VideoPlayer _video;
    public DisplayControl _displayControl;


    private void Start()
    {
        //初始時關閉動畫物件，呼叫此物件時順便打開動畫物件
        _video.gameObject.SetActive(true);
        _video.Play();

    }



    void Update()
    {
        // 收到結束訊號後停止右眼實驗畫面的顯示
        if (!_displayControl._gameStart)
        {
            _video.Pause();
            _video.gameObject.SetActive(false);
        }
    }

}
