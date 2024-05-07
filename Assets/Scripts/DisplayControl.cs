using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.XR;
using Wave.OpenXR;
using TMPro;
using Wave.Essence.Eye;
using System.Threading;



public class LogMessage
{
    public string message;
}


public class DisplayControl : MonoBehaviour
{

    public GameObject _itemsScreen; //右眼畫面物件
    public RawImage rawImage;
    public RawImage Upper;
    public RawImage Lower;

    public GameObject _videoScreen; //左眼畫面物件
    public RawImage _video;


    public GameObject _presentScreen;//展示用畫面物件,only for 展示用(為了顯示兩畫面)


    public GameObject _intro1;//實驗介紹畫面

    public GameObject _intro2;//為了演示雙畫面，用在unity display2


    public float _roundTime = 30; //實驗時間(from start to end per round)


    public int _roundNum; //實驗回合總數設定


    public int _roundCount = 1;//實驗當前回合


    public bool _newRound = true;    //fade out 反應後開始new round


    [HideInInspector]
    public bool _gameStart = false;     //實驗是否開始了
    [HideInInspector]
    //public bool _roundStart = false;    //實驗中每回合的tag，start指示回合開始、end指示回合結束

    private bool _startFadein = false;
    private bool _isRespondF = false;//Fadein階段的反應


    private bool _startFadeout = false;
    private bool _isRespondB = false;//Fadeawat階段的反應


    public bool _makeFadeModeChange = false;//true使fade mode change
    //bool waitingforinput = false;

    //triger訊號紀錄
    public DataManager _dataManager;

    //訊號類別實例
    public LogMessage _logMessage = new LogMessage();

    public TMP_InputField _inputQuizNum;
    public TMP_InputField _inputRoundTime;


    //反應後逼聲
    public AudioSource _respoundBi;

    //幀數變數
    public float showTime = 1f;
   

    private int count = 0;
    private float deltaTime = 0f;

    //測試亮度用
    public GameObject _testBright;
    //present用
    public GameObject _presentModeCanvas;


    //Controller
    public GameObject _rightHandContr;
    public GameObject _leftHandContr;
    public GameObject _intereactionMan;

    /// <summary>
    /// 實驗初始化，introduction的顯示
    /// </summary>
    void Start()
    {
        //GameStart();

        //測試用
        //TestStart();
        //GetComponent<AudioListener>().enabled = false;
    }


    /// <summary>
    /// 等待輸入右搖桿之triger後實驗開始，並設定可隨時停止及重來
    /// </summary>
    void Update()
    {

        //受試者反應紀錄
        //看到出現
        //當實驗開始的過程中，按鍵反應
        if (_gameStart)
        {

            if (Input.anyKeyDown)
            //if (InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) && _gameStart)
            {
                //看到物品出現時，按過後_isRespondF = true
                if (!_isRespondF && _startFadein)
                {
                    _respoundBi.Play();

                    Debug.Log("Notice the target show up : round" + _roundCount.ToString() + " respond time");
                    _isRespondF = true;
                    _logMessage.message = "Notice the target show up : round" + _roundCount.ToString() + " respond time";
                    _dataManager.SaveLogMessage(_logMessage);

                }


                //發現物品消失時，按trigger反應後_isRespondB = true
                if (!_isRespondB && _startFadeout)
                {
                    _respoundBi.Play();

                    Debug.Log("Notice the target disappear : round" + _roundCount.ToString() + " respond time");
                    _isRespondB = true;
                    _newRound = true;
                    _logMessage.message = "Notice the target disappear : round" + _roundCount.ToString() + " respond time";
                    _dataManager.SaveLogMessage(_logMessage);

                }
               

            }

            //紀錄幀數
            CountFPS();


        }


    }


    /// <summary>
    /// RoundCount(當前回合)<=RoundNum(設定回合數)時，實驗開始，直到滿足回合數，
    /// 當_gameStarting表實驗進行，
    /// 當_roundStart表回合開始，漸亮到漸暗各一半_roundTime，
    /// </summary>
    /// <returns></returns>
    private IEnumerator RunExperiment()
    {

        while (_roundCount <= _roundNum+1 && _gameStart == true)
        {
            Debug.Log("wait round " + (_roundCount - 1).ToString() + " notice the target disappear");
            while (!_newRound)
            //while (!Input.anyKey)
            {
                yield return null;
            }
            Debug.Log("get round " + (_roundCount-1).ToString() + " trigger");
            _startFadeout = false;
            if (_roundCount > _roundNum)
            {
                _gameStart = false;

                _roundCount = 1;
                _itemsScreen.SetActive(false);
                rawImage.gameObject.SetActive(false);
                Upper.gameObject.SetActive(false);
                Lower.gameObject.SetActive(false);

                _videoScreen.SetActive(false);
                _video.gameObject.SetActive(false);

                _presentScreen.SetActive(false);//only for 展示用(為了顯示兩畫面)
                _presentModeCanvas.SetActive(false); //..


                _rightHandContr.SetActive(true);
                _leftHandContr.SetActive(true);
                _intereactionMan.SetActive(true);
                _intro1.SetActive(true);
                _intro2.SetActive(true);
                break;
            }


            _isRespondF = false;//每回合都設定獨立的反應trigger，反應過後設為true
            _isRespondB = false;


            //stall until item change and its mean next round is readying

            _makeFadeModeChange = true;
            _startFadein = true;
            yield return new WaitForSeconds(_roundTime/2);
            _startFadein = false;


            _newRound = false;//開始新回合後重設回false

            _makeFadeModeChange = true;
            _startFadeout = true;
            yield return new WaitForSeconds(_roundTime/2);
            


            _roundCount++;
            
            

        }

        _logMessage.message = "Experiment over";
        _dataManager.SaveLogMessage(_logMessage);

        PlayerPrefs.SetInt("GetData", 0);

    }


    public void GameStart()
    {
        ////if (InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) && !_gameStart)
        //if (Input.anyKey && !_gameStart)
        //{
            _roundNum = int.Parse(_inputQuizNum.text);
            _roundTime = int.Parse(_inputRoundTime.text);

            //ThreadStart childref = new ThreadStart(CallToChildThread);
            _intro1.SetActive(false);
            //intro2.SetActive(false);

            //controller disappear
            _rightHandContr.SetActive(false);
            _leftHandContr.SetActive(false);
            _intereactionMan.SetActive(false);

            PlayerPrefs.SetInt("GetData", 1);//Take DataManager on

            _logMessage.message = "Experiment start";
            _dataManager.SaveLogMessage(_logMessage);

            
            _gameStart = true;
            
            //_roundStart = true;

            _newRound = true;
            //Run experiment -> RunExperiment()
            _itemsScreen.SetActive(true);
            _videoScreen.SetActive(true);
            

            StartCoroutine(RunExperiment());
            //After experiment, turn to initial 

    }


    //測試亮度用
    public void TestStart()
    {
        _intro1.SetActive(false);
        _testBright.SetActive(true);
    }


    public void PresentMode()
    {
        _roundNum = int.Parse(_inputQuizNum.text);
        _roundTime = int.Parse(_inputRoundTime.text);

        //ThreadStart childref = new ThreadStart(CallToChildThread);
        _intro1.SetActive(false);
        //intro2.SetActive(false);
        _presentModeCanvas.SetActive(true);


        //controller disappear
        _rightHandContr.SetActive(false);
        _leftHandContr.SetActive(false);
        _intereactionMan.SetActive(false);

        PlayerPrefs.SetInt("GetData", 1);//Take DataManager on

        _logMessage.message = "Presently experiment start";
        _dataManager.SaveLogMessage(_logMessage);


        _gameStart = true;

        //_roundStart = true;

        _newRound = true;
        _presentScreen.SetActive(true);


        StartCoroutine(RunExperiment());
        //After experiment, turn to initial 
    }


    //time threads to avoid delay
    public void CallToChildThread()
    {
        Debug.Log("Child(Time) thread start");
        //每15秒傳送執行的訊號，
        //每30秒等待受試者trigger
        while (true)
        {
            
            if (_roundCount <= _roundNum)
            {
                break;
            }
        }
    }

    public static IEnumerator WaitForSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }

    //計算幀數
    public void CountFPS()
    {
        count++;
        deltaTime += Time.deltaTime;
        if (deltaTime >= showTime)
        {
            
            float milliSecond = deltaTime * 1000 / count;;

            _logMessage.message = ""+milliSecond.ToString();
            _dataManager.SaveFPS(_logMessage);

            count = 0;
            deltaTime = 0f;
        }
    }

}