using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RightEyeDisplay : MonoBehaviour
{
    public FadeInOut m_Fade;
    public ItemChange _itemChange;
    //紀錄實驗次數跟設定實驗次數
    private int RoundCount;
    public int RoundNum;

    //右眼畫面開始依據
    [HideInInspector]
    public bool RoundStart = false;

    public DisplayControl _displayContral;


    void Start()
    {
        RoundStart = true;
    }

    void Update()
    {
        //收到開始訊號後開始右眼實驗畫面的顯示
        while (_displayContral._roundStart)
        {

            StartCoroutine(ExperimentPlay_Right());
        }

    }
    private IEnumerator ExperimentPlay_Right()
    {

        _displayContral._roundStart = false;
        //右眼畫面開始
        _itemChange.Change = true;
        yield return new WaitForSeconds(1.0f);
        m_Fade.BackGroundControl(false);
        //右眼畫面結束

    }
}
