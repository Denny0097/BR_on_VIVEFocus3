using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestBright : MonoBehaviour
{
    public GameObject _intro1;//實驗介紹畫面
    public GameObject _self;
    public DisplayControl _displayControl;
    public RawImage _image;
    public Texture[] Items;
    Texture randomImage;


    public float showTime = 0f;//可用來間隔切換畫面時間，設0時表最短時間取決於幀數
    private float deltaTime = 0f;
    private float count = 0f;//計算總共經過多久
    public int _totalTime = 10;

    private void Start()
    {
        _image.texture = Items[1];
    }

    private void Update()
    {

        deltaTime += Time.deltaTime;
        count += Time.deltaTime;
        //時間到之前都持續保持切換畫面
        if (_totalTime > count)
        {
            //到達設定的時間間隔就切換畫面
            if (deltaTime >= showTime)
            {   //0 = white, 1 = black
                //white to black
                if (_image.texture == Items[0])
                {
                    _image.texture = Items[1];
                }
                //black to white
                else
                {
                    _image.texture = Items[0];
                }
                deltaTime = 0;
            }
        }
        else
        {
            count = 0;
            _intro1.SetActive(true);
            _self.SetActive(false);
        }
    }

   

}
