using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RightEyeDisplay : MonoBehaviour
{
    public DisplayControl _displayControl;
    public FadeInOut m_Fade;
    public ItemChange _itemChange;

    private bool _invoked = false;
    private bool _runned = false;
<<<<<<< HEAD

    


=======




>>>>>>> VideoFix
    void Update()
    {
        //收到開始回合的訊號，右眼內容開始呈現
        if (_displayControl._roundStart && !_invoked)
        {
            _invoked = true;
            _runned = true;
            m_Fade.gameObject.SetActive(true);
            InvokeRepeating("ExperimentPlay_Right", 0f, _displayControl._roundTime);
            InvokeRepeating("MakeFadeChange", 0f, _displayControl._roundTime / 2);
        }

<<<<<<< HEAD
        if(_displayControl._roundCount == _displayControl._roundNum && _runned)
=======
        if (_displayControl._roundCount == _displayControl._roundNum && _runned)
>>>>>>> VideoFix
        {
            _runned = false;
            m_Fade.gameObject.SetActive(false);
            _itemChange.Upper.texture = _itemChange.Items[8];
            _itemChange.Lower.texture = _itemChange.Items[8];

        }
    }


    private void ExperimentPlay_Right()
    {
        // 收到開始訊號後開始右眼實驗畫面的顯示
        // if (_displayControl._roundStart)
        // {
        StartCoroutine(ExperimentPlay_RightCoroutine());
        // }
    }


    //改變 fade in/out 的狀態
    private void MakeFadeChange()
    {
        if (m_Fade.isBlack)
        {
            _displayControl._logMessage.message = "Fade in now";
            _displayControl._dataManager.SaveLogMessage(_displayControl._logMessage);
            m_Fade.isBlack = false;
        }
        else
        {
            _displayControl._logMessage.message = "Fade out now";
            _displayControl._dataManager.SaveLogMessage(_displayControl._logMessage);
            m_Fade.isBlack = true;
        }
    }


    private IEnumerator ExperimentPlay_RightCoroutine()
    {
        _displayControl._roundStart = false;
        _itemChange.ChangeImage();

        //m_Fade.FadeinhThenFadeout();
        yield return null; // 避免編譯器警告
    }
}