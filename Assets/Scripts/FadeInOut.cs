using System.IO;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;//注意添加RawImage命名空間

public class FadeInOut : MonoBehaviour
{
    //public DisplayControl _displayControl;

    [HideInInspector]
    public bool isBlack = false;//不透明狀態
    
    public float fadeSpeed = 0.07f;//透明度變化速率
    public RawImage rawImage;
    public RectTransform rectTransform;
    public Color OriginColor;
    public Color GoalColor;

    void Start()
    { 
        //使背景滿屏
        //rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        OriginColor = rawImage.color ;
        //rawImage.color = Color.white;
    }


    // Update is called once per frame
    void Update()
    {
        if (isBlack)
        {
            //時間計算
            Color newColor = rawImage.color; // 複製原始顏色
            newColor.a = Mathf.LerpUnclamped(rawImage.color.a, GoalColor.a, Time.deltaTime * fadeSpeed);
            if (newColor.a <= 0.15)
                newColor.a = 0.1f;
            rawImage.color = newColor; // 設置新的透明度
            //之所以這麼寫主要是因爲Lerp函數的原因，具體詳解可以看這篇文章
            //【Unity中Lerp的用法】https://blog.csdn.net/MonoBehaviour/article/details/79085547

            if (newColor.a <= 0.1f)
            {
                newColor.a = Mathf.LerpUnclamped(rawImage.color.a, GoalColor.a, Time.deltaTime * (fadeSpeed * 10));
                if (newColor.a <= 0.05)
                    newColor.a = 0.0f;
                rawImage.color = newColor;
            }
            

        }
        if (!isBlack)
        {
            Color newColor = rawImage.color; // 複製原始顏色
            newColor.a = Mathf.LerpUnclamped(rawImage.color.a, OriginColor.a, Time.deltaTime * fadeSpeed);
            if (newColor.a >= 0.85)
                newColor.a = 0.90f;
            rawImage.color = newColor; // 設置新的透明度值

            if (newColor.a >= 0.90)
            {
                newColor.a = Mathf.LerpUnclamped(rawImage.color.a, OriginColor.a, Time.deltaTime * (fadeSpeed * 10));
                if (newColor.a >= 0.95)
                    newColor.a = 1f;
                rawImage.color = newColor;
            }
            
        }
    }


    //單次實驗的fadinout
    //改成用isactive開關就好
    public void FadeinhThenFadeout()
    {
        
        if (isBlack)
        {
            Fadein();
        }
        if (!isBlack)
        {
            Fadeout();
        }
        // yield return null;
       
    }

    public void Fadein()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, Time.deltaTime * 15);
        //漸亮
        //之所以這麼寫主要是因爲Lerp函數的原因，具體詳解可以看這篇文章
        //【Unity中Lerp的用法】https://blog.csdn.net/MonoBehaviour/article/details/79085547
        if (rawImage.color.a < 0.1f)
        {
            rawImage.color = Color.clear;
            isBlack = false;
        }
    }

    public void Fadeout()
    {
        rawImage.color = Color.Lerp(rawImage.color, OriginColor, Time.deltaTime * 15);//漸暗
        if (rawImage.color.a > 0.9f)
        {
            rawImage.color = Color.black;
            isBlack = true;
        }

    }

    private IEnumerator Fadein(float Displaytime)
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, Time.deltaTime * Displaytime/2);
        //漸亮
        //之所以這麼寫主要是因爲Lerp函數的原因，具體詳解可以看這篇文章
        //【Unity中Lerp的用法】https://blog.csdn.net/MonoBehaviour/article/details/79085547
        if (rawImage.color.a < 0.1f)
        {
            rawImage.color = Color.clear;
            isBlack = true;
        }
        yield return null;
    }

    private IEnumerator Fadeout(float DisplayTime)
    {
        rawImage.color = Color.Lerp(rawImage.color, OriginColor, Time.deltaTime * DisplayTime/2);//漸暗
        if (rawImage.color.a > 0.9f)
        {
            rawImage.color = Color.black;
            isBlack = false;
        }
        yield return null;
    }


    //切換狀態
    public void BackGroundControl(bool b)
    {
        if (b == true)
            isBlack = true;
        else
            isBlack = false;
    }
}