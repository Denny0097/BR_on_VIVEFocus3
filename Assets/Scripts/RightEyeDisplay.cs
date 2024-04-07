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


    
    void Update()
    {
        //收到開始回合的訊號，右眼內容開始呈現
        if (_displayControl._roundStart&&!_invoked)
        {
            _invoked = true;
            m_Fade.gameObject.SetActive(true);
            InvokeRepeating("ExperimentPlay_Right", 0f, _displayControl._roundTime);
            InvokeRepeating("MakeFadeChange", 0f, _displayControl._roundTime / 2);
            if (_displayControl._roundCount == _displayControl._roundNum)
            {

                m_Fade.gameObject.SetActive(false);

            }
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
            m_Fade.isBlack = false;
        }
        else
        {
            m_Fade.isBlack = true;
        }
    }


    private IEnumerator ExperimentPlay_RightCoroutine()
    {
        _displayControl._roundCount++;
        _itemChange.ChangeImage();

        //m_Fade.FadeinhThenFadeout();
        yield return null; // 避免編譯器警告
    }
}
