using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading;
using UnityEngine.UI;


public class ItemDisplayController : MonoBehaviour
{
    /// Fade used
    [HideInInspector]
    public bool isBlack = false;//不透明狀態

    public float _fadeSpeed = 0.07f;//透明度變化速率
    public RawImage rawImage;

    public Color OriginColor;
    public Color GoalColor;
    /// </summary>

    public DisplayControl _displayControl;
    public FadeInOut m_Fade;
    public ItemChange _itemChange;

    //private bool _invoked = false;
    private bool _runned = false;

    //反應後逼聲
    public AudioSource _changeModeBi;


    private void Start()
    {
        rawImage.gameObject.SetActive(true);
        OriginColor = rawImage.color;
        _fadeSpeed = 1 / (_displayControl._roundTime / 2);


        //m_Fade.gameObject.SetActive(true);
    }


    void Update()
    {
        CountFadeRate();
        if (_displayControl._gameStart)
        {
            //收到開始回合的訊號，右眼內容開始呈現
            if (_displayControl._makeFadeModeChange)
            {
                //每次發出respound都會有一個bi聲
                _changeModeBi.Play();
                _displayControl._makeFadeModeChange = false;
                _runned = true;
            
                MakeFadeChange();
            }

            if (_displayControl._roundCount == _displayControl._roundNum && _runned)
            {
                _runned = false;
                //m_Fade.gameObject.SetActive(false);
                _itemChange.Upper.texture = _itemChange.Items[8];
                _itemChange.Lower.texture = _itemChange.Items[8];

            }
        

            if (isBlack )
            {
                Fadein();
            }
            else
            {

                Fadeout();
            }
        }
    }




    //改變 fade in/out 的狀態
    private void MakeFadeChange()
    {
        
        if (!isBlack)
        {
            _itemChange.ChangeImage();
            Debug.Log("Round" + _displayControl._roundCount.ToString() + " Change mode to Fade in");
            _displayControl._logMessage.message = "Round" + _displayControl._roundCount.ToString() + " Fade in now";
            _displayControl._dataManager.SaveLogMessage(_displayControl._logMessage);
            isBlack = true; 
        }
        else
        {

            Debug.Log("Round" + _displayControl._roundCount.ToString() + " Change mode to Fade out");
            _displayControl._logMessage.message = "Round " + _displayControl._roundCount.ToString() + " Fade out now";
            _displayControl._dataManager.SaveLogMessage(_displayControl._logMessage);
            isBlack = false;
           
        }
    }


    public void Fadein()
    {
        if (rawImage.color.a > 0)
        {

            Color newColor = rawImage.color; // 複製原始顏色
            float t = Time.deltaTime * _fadeSpeed;
            newColor.a -= t;
            rawImage.color = newColor;
        }

    }

    //Cover越來越淺，畫面越來越清楚
    public void Fadeout()
    {
        if(rawImage.color.a < 1)
        {

            Color newColor = rawImage.color; // 複製原始顏色
            float t = Time.deltaTime * _fadeSpeed;
            newColor.a += t;
            rawImage.color = newColor;
        }
       
    }

    public IEnumerator WaitForFrame()
    {

        yield return null;
    }

    public void CountFadeRate()
    {
        
        _displayControl._logMessage.message = ""+rawImage.color.a.ToString();
        _displayControl._dataManager.SaveFadeRate(_displayControl._logMessage);

        
    }

}