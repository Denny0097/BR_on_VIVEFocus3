using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestBright : MonoBehaviour
{
    public GameObject _intro1;//實驗介紹畫面
    public GameObject _self;
    public DisplayControl _displayControl;
    public RawImage _image;
    public Texture[] Items;


    public float showTime = 0f;//可用來間隔切換畫面時間，設0時表最短時間取決於幀數
    private float deltaTime = 0f;
    private float count = 0f;//計算總共經過多久
    public int _totalTime = 20;
    public TMP_InputField _inputfield;
    private float flashTime;


    //次數
    int _count = 0;
    private void Start()
    {
        flashTime = showTime - (float.Parse(_inputfield.text) / 1000);
    }
    private void Update()
    {
        
        deltaTime += Time.deltaTime;
        //count += Time.deltaTime;
        //時間到之前都持續保持切換畫面
        if (_totalTime > _count )
        {
            if( deltaTime >= showTime)
            {
                // 切換畫面
                if (_image.texture == Items[0])
                {
                    _image.texture = Items[1];
                    deltaTime = 0;
                    _count += 1;
                }
                else
                {
                    _image.texture = Items[0];
                    deltaTime = flashTime;
                    _count += 1;
                }
            }
            
            
        }
        else
        {
            _count = 0;
            _intro1.SetActive(true);
            _self.SetActive(false);
        }
    }

   

}
