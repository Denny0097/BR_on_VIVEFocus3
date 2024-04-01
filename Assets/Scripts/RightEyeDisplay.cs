using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RightEyeDisplay : MonoBehaviour
{
    public FadeInOut m_Fade;
    public ItemChange _itemChange;
    //紀錄實驗次數跟設定實驗次數
    private int RoundCount = 0;
    public int RoundNum;

    //右眼畫面開始依據
    [HideInInspector]
    public bool RoundStart = false;

    private bool _invoked = false;

    //public DisplayControl _displayControl;

    void Update()
    {
        if (!_invoked)
        {
            _invoked = true;
            m_Fade.gameObject.SetActive(true);
            InvokeRepeating("ExperimentPlay_Right", 0f, 30f);
            InvokeRepeating("MakeFadeChange", 0f, 15f);
        }

        if(RoundCount == RoundNum)
        {

            m_Fade.gameObject.SetActive(false);
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
        RoundCount++;
        _itemChange.ChangeImage();

        //m_Fade.FadeinhThenFadeout();
        yield return null; // 避免編譯器警告
    }
}
