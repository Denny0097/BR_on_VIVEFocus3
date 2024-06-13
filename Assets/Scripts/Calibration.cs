using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Wave.OpenXR;
using TMPro;

public class Calibration : MonoBehaviour
{
    public RectTransform _target;

    public Text _instruction;
    public GameObject _targetdot;
    public GameObject _clock;

    public GameObject _initialUI;

    //public GameObject _gamecontrol;
    private float Speed;
    
    public DataManager _CaliData;
    public LogMessage _logMessage = new LogMessage();

    int count = 0;
    float RowStep = 200;
    float ColStep = 200;
    float canvas_dist = 100;


    public void CallCaliOne()
    {
        StartCoroutine(Cali_One());
    }
    public void CallCaliTwo()
    {
        StartCoroutine(Cali_Two());
    }

    public IEnumerator Cali_One()
    {
        _initialUI.SetActive(false);
        //_instruction.gameObject.SetActive(true);
        //Cali1 intruc
        //...
        _clock.gameObject.SetActive(true);

        yield return StartCoroutine(Take_A_Break());

        _clock.gameObject.SetActive(false);
        _instruction.gameObject.SetActive(false);

        _initialUI.SetActive(true);
        //Cali1 intruc
        //...
    }

    public IEnumerator Cali_Two()
    {

        Vector3 currentPosition = _target.anchoredPosition;

        _initialUI.SetActive(false);
        _instruction.gameObject.SetActive(true);
        _instruction.text = "準備進行9點眼動校正\n請按鍵開始";

        yield return StartCoroutine(Take_A_Break());


        _instruction.gameObject.SetActive(false);

        _targetdot.SetActive(true);

        
        _logMessage.message = "Order of presentation : 右 左 上 下 右上 右下 左上 左下 中";
        _CaliData.SaveLogMessage(_logMessage);

        yield return StartCoroutine(TargetShow(currentPosition));

        _initialUI.SetActive(true);
    }


    private IEnumerator TargetShow(Vector3 currentPosition)
    {
        PlayerPrefs.SetInt("GetData", 1);

        while (count <= 9)
        {


            switch (count)
            {
                case 1:
                    _target.anchoredPosition = new Vector3(RowStep, 0, 0);
                    _logMessage.message = "dot position(" + RowStep.ToString() + ",0," + canvas_dist.ToString() + "), right";
                    _CaliData.SaveLogMessage(_logMessage);
                    yield return null;
                    break;
                case 2:
                    _target.anchoredPosition = new Vector3(-RowStep, 0, 0);
                    _logMessage.message = "dot position(-" + RowStep.ToString() + ",0," + canvas_dist.ToString() + "), left";
                    _CaliData.SaveLogMessage(_logMessage);

                    break;
                case 3:
                    _target.anchoredPosition = new Vector3(0, ColStep, 0);
                    _logMessage.message = "dot position(0," + ColStep.ToString() + "," + canvas_dist.ToString() + "), top";
                    _CaliData.SaveLogMessage(_logMessage);

                    break;
                case 4:
                    _target.anchoredPosition = new Vector3(0, -ColStep, 0);
                    _logMessage.message = "dot position(0,-" + ColStep.ToString() + "," + canvas_dist.ToString() + "), buttom";
                    _CaliData.SaveLogMessage(_logMessage);

                    break;
                case 5:
                    _target.anchoredPosition = new Vector3(RowStep, ColStep, 0);
                    _logMessage.message = "dot position(" + RowStep.ToString() + "," + ColStep.ToString() + "," + canvas_dist.ToString() + "), upper right";
                    _CaliData.SaveLogMessage(_logMessage);

                    break;
                case 6:
                    _target.anchoredPosition = new Vector3(RowStep, -ColStep, 0);
                    _logMessage.message = "dot position(" + RowStep.ToString() + ",-" + ColStep.ToString() + "," + canvas_dist.ToString() + "), lower right";
                    _CaliData.SaveLogMessage(_logMessage);

                    break;
                case 7:
                    _target.anchoredPosition = new Vector3(-RowStep, ColStep, 0);
                    _logMessage.message = "dot position(-" + RowStep.ToString() + "," + ColStep.ToString() + "," + canvas_dist.ToString() + "), upper left";
                    _CaliData.SaveLogMessage(_logMessage);

                    break;
                case 8:
                    _target.anchoredPosition = new Vector3(-RowStep, -ColStep, 0);
                    _logMessage.message = "dot position(-" + RowStep.ToString() + ",-" + ColStep.ToString() + "," + canvas_dist.ToString() + "), lower left";
                    _CaliData.SaveLogMessage(_logMessage);

                    break;
                case 9:
                    _target.anchoredPosition = new Vector3(0, 0, 0);
                    _logMessage.message = "dot position(0,0," + canvas_dist.ToString() + "), center";
                    _CaliData.SaveLogMessage(_logMessage);

                    break;


            }


            count++;

            _targetdot.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            _targetdot.SetActive(false);

            yield return new WaitForSeconds(0.5f);
        }

        _targetdot.SetActive(false);
        PlayerPrefs.SetInt("GetData", 0);
        _instruction.gameObject.SetActive(true);
        _instruction.text = "矯正結束";
        yield return new WaitForSeconds(4.0f);
        _instruction.text = "";

    }

    private IEnumerator Take_A_Break()
    {
        while (!Input.anyKey)
        //while (!InputDeviceControl.KeyDown(InputDeviceControl.ControlDevice.Right, CommonUsages.triggerButton) || !Input.anyKey)
        {
            yield return null;
        }

    }

}
