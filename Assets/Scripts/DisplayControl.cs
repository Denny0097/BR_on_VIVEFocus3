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


public class DisplayContral : MonoBehaviour
{
    //兩眼畫面物件
    public RightEyeDisplay _itemsScreen;
    public LeftEyeDisplay _videoScreen;
    //
    public GameObject intro;
    


    public int RoundNum;
    private int RoundCount = 1;
    //是否開始了
    [HideInInspector]
    public bool _gameStarting = false;
    //每round的tag
    public bool _roundStart = false;
    public bool _roundEnd = false;

    bool waitingforinput = false;

    public DataManager _dataManager;
    public LogMessage _logMessage = new LogMessage();

    void Start()
    {
        
    }



    void Update()
    {
        if ((Input.anyKey || InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton)) && !_gameStart)
        //if (Input.anyKey &&!_gameStart)
        {
            _gameStart = true;
            StartCoroutine(ExperimentPlay());

        }

    }


    private IEnumerator ExperimentPlay()
    {
        while(RoundCount <= RoundNum){

            //輸入後開始畫面
            yield return StartCoroutine(WaitUntilInput());
            _itemsScreen.RoundStart = true;
            _videoScreen.PlayVideo();
        }


    }


    private IEnumerator WaitUntilInput()
    {
        yield return new WaitForSeconds(2.0f);
        while (!InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) || !Input.anyKey)
        //while (!Input.anyKey)
        {
            yield return null;
        }

    }

}