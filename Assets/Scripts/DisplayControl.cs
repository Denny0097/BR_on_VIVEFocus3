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
    //test calibration
    public Calibration _cali;
    public GameObject _caliCanvas_R;
    public GameObject _caliCanvas_L;


    public GameObject _itemsScreen; //右眼畫面物件
    public RawImage rawImage;
    public RawImage[] _images;
    public RawImage _central;


    public GameObject _videoScreen; //左眼畫面物件
    public RawImage _video;



    public GameObject _intro1;//實驗介紹畫面
    public GameObject _intro2;//為了演示雙畫面，用在unity display2
    public Text _restInstruct;
    public RawImage _restTexture;

    [HideInInspector]
    public float _roundTime = 30; //實驗時間(from start to end per round)

    [HideInInspector]
    public int _numberOfTrial; //實驗回合總數設定

    [HideInInspector]
    public int _roundCount = 1;//實驗當前回合

    [HideInInspector]
    public bool _newRound = true;    //fade out 反應後開始new round

    [HideInInspector]
    public bool _gameStart = false;     //實驗是否開始了

    [HideInInspector]
    public bool _gameStop = false;      //實驗是否暫停

    
    private bool _startFadein = false;
    private bool _isRespondF = false;//Fadein階段的反應

    private bool _startFadeout = false;
    private bool _isRespondB = false;//Fadeawat階段的反應

    [HideInInspector]
    public bool _makeFadeModeChange = false;//true使fade mode change
    

    
    public DataManager _dataManager;//triger訊號紀錄

    //訊號類別實例
    public LogMessage _logMessage = new LogMessage();

    public TMP_InputField _inputQuizNum;
    public TMP_InputField _inputRoundTime;
    public TMP_InputField _locationNum;

    //反應後逼聲
    public AudioSource _respoundBi;

    ////幀數變數
    //public float showTime = 1f;

    //private int count = 0;
    //private float deltaTime = 0f;

    ////測試亮度用
    public GameObject _testBright;
    ////present用
    //public GameObject _presentModeCanvas;

    //Controller
    public GameObject _rightHandContr;
    public GameObject _leftHandContr;
    public GameObject _intereactionMan;


    //判斷是否初次進行實驗
    bool isFirst = true;
    

    /// <summary>
    /// 實驗初始化，introduction的顯示
    /// </summary>
    void Start()
    {
        //GameStart();

        //測試用
        //TestStart();
        //GetComponent<AudioListener>().enabled = false;

        //Calibration
        //_cali.CallCaliTwo();
        
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

                    _isRespondF = true;
                    _logMessage.message = "Notice the appearance.";
                    _dataManager.SaveLogMessage(_logMessage);

                }


                //發現物品消失時，按trigger反應後_isRespondB = true
                if (!_isRespondB && _startFadeout)
                {
                    _respoundBi.Play();

                    _isRespondB = true;
                    _newRound = true;
                    _logMessage.message = "Notice the disappearance.";
                    _dataManager.SaveLogMessage(_logMessage);

                }      
            }
        }
    }


    /// <summary>
    /// RoundCount(當前回合)<=RoundNum(設定回合數)時，實驗開始，直到滿足回合數，
    /// 當_gameStarting表實驗進行，
    /// 當_roundStart表回合開始，漸亮到漸暗各一半_roundTime，
    /// </summary>
    /// <returns></returns>
    ///

    private IEnumerator TrialStart()
    {
        _isRespondF = false;//每回合都設定獨立的反應trigger，反應過後設為true
        _isRespondB = false;


        //stall until item change and its mean next round is readying

        _makeFadeModeChange = true;
        _startFadein = true;
        yield return new WaitForSeconds(_roundTime / 2);
        _startFadein = false;


        _newRound = false;//開始新回合後重設回false

        _makeFadeModeChange = true;
        _startFadeout = true;
        yield return new WaitForSeconds(_roundTime / 2);

    }

    public void ExperimentTerminate()
    {
        _gameStart = false;

        _roundCount = 1;
        _itemsScreen.SetActive(false);
        rawImage.gameObject.SetActive(false);
        if (int.Parse(_locationNum.text) == 1)
        {
            _central.gameObject.SetActive(false);
        }
        else if (int.Parse(_locationNum.text) == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                _images[i].gameObject.SetActive(false);
            }
        }
        _videoScreen.SetActive(false);
        _video.gameObject.SetActive(false);


        _rightHandContr.SetActive(true);
        _leftHandContr.SetActive(true);
        _intereactionMan.SetActive(true);
        _intro1.SetActive(true);
        //_intro2.SetActive(true);
    }



    private IEnumerator RunExperiment()
    {
        _gameStop = false;
        int Limit;

        if (isFirst)
        {
            Limit = 2;
        }
        else
        {
            Limit = _numberOfTrial;
        }


        while (_roundCount <= Limit + 1 && _gameStart == true)
        {
            _logMessage.message = "Round " + (_roundCount).ToString();
            _dataManager.SaveLogMessage(_logMessage);

            //在做出反應前不繼續下個trial
            while (!_newRound)
            //while (!Input.anyKey)
            {
                yield return null;
            }

            _startFadeout = false;

            if (_roundCount > Limit)
            {
                if (!isFirst)
                {
                    ExperimentTerminate();                    
                }
                else
                {
                    _gameStop = true;
                    //show text and canvas to overlay all screen items
                    Debug.Log("Practice is done.");
                    _restInstruct.text = "Practice completed,\n click trigger to start experiment";
                    _restInstruct.gameObject.SetActive(true);

                    //先暫時遮住物件，而不是消失，這樣不用重新呼喚物件
                    _restTexture.enabled = true;
                    //至少等待1s，避免按鍵多次反應
                    _respoundBi.Play();
                    yield return StartCoroutine(WaitForSeconds(0.5f));
                    _respoundBi.Play();
                }

                break;

            }

            yield return StartCoroutine(TrialStart());
            _roundCount++;

        }


        _logMessage.message =  isFirst ? "Practice completed": "Experiment completed";
        _dataManager.SaveLogMessage(_logMessage);
        Debug.Log(_logMessage.message);

        _roundCount = 1;

        if (!isFirst)
        {
            PlayerPrefs.SetInt("GetData", 0);
        }

        isFirst = false;
    }


    private IEnumerator PracticeAndFormal()
    {

        Debug.Log("Practice 2 trial.");
        _logMessage.message = "Practice 2 trial";
        _dataManager.SaveLogMessage(_logMessage);
        yield return StartCoroutine(RunExperiment());

        yield return StartCoroutine(Take_A_Break());
        _restInstruct.gameObject.SetActive(false);
        _restTexture.enabled = false;

        Debug.Log("Formal experiment begin");
        _logMessage.message = "Formal experiment begin";
        _dataManager.SaveLogMessage(_logMessage);
        yield return StartCoroutine(RunExperiment());
        //After experiment, turn to initial 


    }



    public void GameStart()
    {

        _numberOfTrial = int.Parse(_inputQuizNum.text);
        _roundTime = int.Parse(_inputRoundTime.text);


        _intro1.SetActive(false);
        //intro2.SetActive(false);
        _caliCanvas_R.SetActive(false);
        _caliCanvas_L.SetActive(false);

        //VR controller disappear
        _rightHandContr.SetActive(false);
        _leftHandContr.SetActive(false);
        _intereactionMan.SetActive(false);

        //Take DataManager on
        PlayerPrefs.SetInt("GetData", 1);

        Debug.Log("Experiment start");
        _logMessage.message = "Experiment start";
        _dataManager.SaveLogMessage(_logMessage);

            
        _gameStart = true;
            
        //_roundStart = true;

        _newRound = true;
        //Run experiment -> RunExperiment()
        _itemsScreen.SetActive(true);
        _videoScreen.SetActive(true);

        rawImage.gameObject.SetActive(true);
        if (int.Parse(_locationNum.text) == 1)
        {
            _central.gameObject.SetActive(true);
        }
        else if (int.Parse(_locationNum.text) == 4)
        {
            for (int i = 0; i < 4; i++)
            {
                _images[i].gameObject.SetActive(true);
            }
        }
        _video.gameObject.SetActive(true);

        StartCoroutine(PracticeAndFormal());


    }


    //測試亮度用
    public void TestStart()
    {
        _intro1.SetActive(false);
        _testBright.SetActive(true);
    }



    public static IEnumerator WaitForSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }



    private IEnumerator Take_A_Break()
    {
        while (!Input.anyKey)
        //while (!InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) || !Input.anyKey)
        {
            yield return null;
        }
    }


    //計算幀數
    //public void CountFPS()
    //{
    //  count++;
    //  deltaTime += Time.deltaTime;
    //  if (deltaTime >= showTime)
    //  {

    //      float milliSecond = deltaTime * 1000 / count;;

    //      _logMessage.message = ""+milliSecond.ToString();
    //      _dataManager.SaveFPS(_logMessage);

    //      count = 0;
    //      deltaTime = 0f;
    //  }
    //}
    //
}