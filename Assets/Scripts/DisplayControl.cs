using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.XR;
using Wave.OpenXR;
using TMPro;
using Wave.Essence.Eye;


public class LogMessage
{
    public string message;
}


public class DisplayControl : MonoBehaviour
{
    //兩眼畫面物件
    public GameObject _itemsScreen;
    public GameObject _videoScreen;

    //實驗介紹畫面
    public GameObject intro;

    //實驗時間(from start to end per round)
    public float _roundTime = 30;

    //實驗回合總數設定
    public int RoundNum;

    //實驗當前回合
    private int RoundCount = 1;
    
    [HideInInspector]//實驗是否開始了
    public bool _gameStart = false;


    //實驗中每回合的tag，start指示回合開始、end指示回合結束
    public bool _roundStart = false;
    public bool _roundEnd = false;


    //bool waitingforinput = false;

    //triger訊號紀錄
    public DataManager _dataManager;

    //訊號類別實例
    public LogMessage _logMessage = new LogMessage();



    /// <summary>
    /// 實驗初始化，introduction的顯示
    /// </summary>
    void Start()
    {
        
    }


    /// <summary>
    /// 等待輸入右搖桿之triger後實驗開始，並設定可隨時停止及重來
    /// </summary>
    void Update()
    {
        //按triger開始實驗
        //if (InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) && !_gameStart)
        if (Input.anyKey &&!_gameStart)
        {
            intro.SetActive(false);

            _gameStart = true;
            _itemsScreen.SetActive(true);
            _videoScreen.SetActive(true);

        }
        //暫停再按a重來
        //
    }

    /// <summary>
    /// RoundCount(當前回合)<=RoundNum(設定回合數)時，實驗開始，直到滿足回合數，
    /// 當_gameStarting表實驗進行，
    /// 當_roundStart表回合開始，漸亮到漸暗各一半_roundTime，
    /// 回合結束後設定_roundEnd = true、RoundCount++，自動繼續下回合，
    /// 但在實驗過程中，如果按a鍵，可以即時暫停
    /// </summary>
    /// <returns></returns>
    private IEnumerator RunExperiment()
    {
        PlayerPrefs.SetInt("GetData", 1);
        _logMessage.message = "Experiment start";
        _dataManager.SaveLogMessage(_logMessage);

        while (RoundCount <= RoundNum && _gameStart == true)
        {
            //按a暫停
            //
            //if (InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, WVR_InputId.))
            //yield return StartCoroutine(WaitUntilInput());


            //自動繼續
            _roundStart = true;
            _roundStart = false;

            _logMessage.message = "round" + RoundCount.ToString() + " start";
            _dataManager.SaveLogMessage(_logMessage);



            yield return new WaitForSeconds(_roundTime);



            _logMessage.message = "round" + RoundCount.ToString() + " end";
            _dataManager.SaveLogMessage(_logMessage);


            RoundCount++;
            if (RoundCount == RoundNum)
                _gameStart = false;

        }

        PlayerPrefs.SetInt("GetData", 0);



    }


    private IEnumerator WaitUntilInput()
    {
        //按b繼續的設定
        //
        //
        yield return new WaitForSeconds(2.0f);
        while (!InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) || !Input.anyKey)
        //while (!Input.anyKey)
        {
            yield return null;
        }

    }

}