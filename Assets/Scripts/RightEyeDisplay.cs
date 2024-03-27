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



    void Start()
    {
        RoundStart = true;
    }

    void Update()
    {
        if (RoundStart)
        {

            for (RoundCount = 1; RoundCount <= RoundNum; RoundCount++)
            {
                //右眼畫面開始
                _itemChange.Change = true;
                m_Fade.BackGroundControl(false);
                //右眼畫面結束
            }
        }

    }


}
