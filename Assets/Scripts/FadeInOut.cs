using UnityEngine;
using UnityEngine.UI;//注意添加RawImage命名空間

public class FadeInOut : MonoBehaviour
{
    public float DurationTime = 15;
    [HideInInspector]
    public bool isBlack = false;//不透明狀態
    [HideInInspector]
    public float fadeSpeed = 1;//透明度變化速率
    public RawImage rawImage;
    public RectTransform rectTransform;
    private Color Original;

    void Start()
    {
        //計算時間
        float FadeDuration = DurationTime / 2;
        fadeSpeed = 1 / FadeDuration;
        //使背景滿屏
        //rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        Original = rawImage.color ;
        //rawImage.color = Color.white;
    }


    // Update is called once per frame
    void Update()
    {
        if (isBlack == false)
        {
            rawImage.color = Color.Lerp(rawImage.color, Color.clear, Time.deltaTime * fadeSpeed);//漸亮
            //之所以這麼寫主要是因爲Lerp函數的原因，具體詳解可以看這篇文章
            //【Unity中Lerp的用法】https://blog.csdn.net/MonoBehaviour/article/details/79085547
            if (rawImage.color.a < 0.1f)
            {
                rawImage.color = Color.clear;
                isBlack = true;
            }
        }
        else if (isBlack)
        {
            rawImage.color = Color.Lerp(rawImage.color, Original, Time.deltaTime * fadeSpeed);//漸暗
            if (rawImage.color.a > 0.9f)
            {
                rawImage.color = Color.black;
            }
        }
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