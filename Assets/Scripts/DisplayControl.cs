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

    public GameObject _itemsScreen; //右眼畫面物件
    public GameObject _videoScreen; //左眼畫面物件
<<<<<<< HEAD
    
    
    public GameObject _intro1;//實驗介紹畫面
=======


    public GameObject _intro1;//實驗介紹畫面

>>>>>>> VideoFix
    public GameObject _intro2;


    public float _roundTime = 30; //實驗時間(from start to end per round)


    public int _roundNum; //實驗回合總數設定


    public int _roundCount = 1;//實驗當前回合


    [HideInInspector]
    public bool _gameStart = false;     //實驗是否開始了
    [HideInInspector]
    public bool _roundStart = false;    //實驗中每回合的tag，start指示回合開始、end指示回合結束
<<<<<<< HEAD
    
=======

>>>>>>> VideoFix
    private bool _isRespond = false;
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
        //GetComponent<AudioListener>().enabled = false;
    }


    /// <summary>
    /// 等待輸入右搖桿之triger後實驗開始，並設定可隨時停止及重來
    /// </summary>
    void Update()
    {
        //按triger開始實驗
        //if (InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) && !_gameStart)
        if (Input.anyKey && !_gameStart)
        {
            _intro1.SetActive(false);
            _intro2.SetActive(false);

            PlayerPrefs.SetInt("GetData", 1);//Take DataManager on

            _logMessage.message = "Experiment start";
            _dataManager.SaveLogMessage(_logMessage);


            _gameStart = true;

            _roundStart = true;


            //Run experiment -> RunExperiment()
            _itemsScreen.SetActive(true);
            _videoScreen.SetActive(true);

            StartCoroutine(RunExperiment());
            //After experiment, turn to initial 


        }

        //受試者反應紀錄
        if (InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) && !_isRespond)
        {
            _isRespond = true;
            _logMessage.message = "round" + _roundCount.ToString() + " respond time";
            _dataManager.SaveLogMessage(_logMessage);

        }

        //暫停再按a重來
        ///
<<<<<<< HEAD
        
=======

>>>>>>> VideoFix


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

        while (_roundCount <= _roundNum && _gameStart == true)
        {
            _roundStart = true;

            _isRespond = false;//每回合都設定獨立的反應按鈕

            _logMessage.message = "round" + _roundCount.ToString() + " start";
            _dataManager.SaveLogMessage(_logMessage);


            //stall until item change and its mean next round is readying
<<<<<<< HEAD
            yield return new WaitForSeconds(_roundTime );
=======
            yield return new WaitForSeconds(_roundTime);
>>>>>>> VideoFix


            _logMessage.message = "round" + _roundCount.ToString() + " over";
            _dataManager.SaveLogMessage(_logMessage);


            _roundCount++;

            if (_roundCount == _roundNum)
            {
                _gameStart = false;
                _roundStart = false;
                _roundCount = 1;
                _intro1.SetActive(true);
                _intro2.SetActive(true);
            }

        }

        _logMessage.message = "Experiment over";
        _dataManager.SaveLogMessage(_logMessage);

        PlayerPrefs.SetInt("GetData", 0);
<<<<<<< HEAD
        
=======

>>>>>>> VideoFix
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