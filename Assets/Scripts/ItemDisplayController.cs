using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Threading;
using UnityEngine.UI;


public class ItemDisplayController : MonoBehaviour
{
    /// Fade used
    [HideInInspector]
    public bool isBlack = false;//不透明狀態

    public float _fadeSpeed = 0.07f;//透明度變化速率
    public RawImage _rawImage;

    public Color OriginColor;
    private Color GoalColor;
    /// </summary>

    public DisplayControl _displayControl;

    public ItemChange _itemChange;

    //private bool _invoked = false;
    private bool _runned = false;

    //反應後逼聲
    public AudioSource _changeModeBi;

    //黑色背景的位置
    private string CoverPath = Path.Combine(Application.persistentDataPath, "Image", "Bcakground.png");
    //private string CoverPath = "Assets/Resources/Image/Background.png";


    private void Start()
    {

        _rawImage.gameObject.SetActive(true);
        //Find cover texture
        LoadImageFromFile(CoverPath, _rawImage);

        Color newColor = _rawImage.color;
        newColor.a = 0;
        //Setting Fade in goal and Original color
        GoalColor = newColor;
        OriginColor = _rawImage.color;

        _fadeSpeed = 1 / (_displayControl._roundTime / 2);


        //m_Fade.gameObject.SetActive(true);
    }


    void Update()
    {

        if (_displayControl._gameStart && !_displayControl._gameStop)
        {
            //收到開始回合的訊號，右眼內容開始呈現
            if (_displayControl._makeFadeModeChange)
            {
                //每次發出respound都會有一個bi聲
                //_changeModeBi.Play();
                _displayControl._makeFadeModeChange = false;
                _runned = true;

                MakeFadeChange();
            }

            //if (_displayControl._roundCount == _displayControl._roundNum && _runned)
            //{
            //    _runned = false;
            //    //m_Fade.gameObject.SetActive(false);
            //    _itemChange.Upper.texture = _itemChange.Items[8];
            //    _itemChange.Lower.texture = _itemChange.Items[8];
            //
            //}


            if (isBlack)
            {
                Fadein();
            }
            else
            {

                Fadeout();
            }
        }
    }


    private void LoadImageFromFile(string filePath, RawImage image)
    {
        
        if (File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(2, 2);
            if (texture.LoadImage(fileData))
            {
                image.texture = texture;
                image.SetNativeSize(); // 可選，根據圖片的原始尺寸調整 RawImage 的大小
            }
            else
            {
                Debug.LogError("Failed to load texture from " + filePath);
            }
        }
        else
        {
            Debug.LogError("File does not exist at " + filePath);
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
        if (_rawImage.color.a > 0)
        {

            Color newColor = _rawImage.color; // 複製原始顏色
            float t = Time.deltaTime * _fadeSpeed;
            newColor.a -= t;
            _rawImage.color = newColor;
        }

    }

    //Cover越來越淺，畫面越來越清楚
    public void Fadeout()
    {
        if(_rawImage.color.a < 1)
        {

            Color newColor = _rawImage.color; // 複製原始顏色
            float t = Time.deltaTime * _fadeSpeed;
            newColor.a += t;
            _rawImage.color = newColor;
        }
       
    }

    public IEnumerator WaitForFrame()
    {

        yield return null;
    }

}