using System.IO;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;//注意添加RawImage命名空間

public class FadeInOut : MonoBehaviour
{
    public DisplayControl _displayControl;

    [HideInInspector]
    public bool isBlack = false;//不透明狀態

    public float _fadeSpeed = 0.07f;//透明度變化速率
    public RawImage rawImage;

    public Color OriginColor;
    public Color GoalColor;

    void Start()
    {
        rawImage.gameObject.SetActive(true);
<<<<<<< HEAD
        OriginColor = rawImage.color ;
        _fadeSpeed = 1 / (_displayControl._roundTime/2);
=======
        OriginColor = rawImage.color;
        _fadeSpeed = 1 / (_displayControl._roundTime / 2);
>>>>>>> VideoFix
    }


    // Update is called once per frame
    void Update()
    {
        if (isBlack)
        {
            Fadein();
        }
        else
        {
            Fadeout();
        }
    }

    public void Fadein()
    {
<<<<<<< HEAD
       
=======

>>>>>>> VideoFix
        Color newColor = rawImage.color; // 複製原始顏色
        float t = Time.deltaTime * _fadeSpeed;
        newColor.a -= t;
        rawImage.color = newColor;

    }

    public void Fadeout()
    {

<<<<<<< HEAD
        
=======

>>>>>>> VideoFix
        Color newColor = rawImage.color; // 複製原始顏色
        float t = Time.deltaTime * _fadeSpeed;
        newColor.a += t;
        rawImage.color = newColor;

    }

}