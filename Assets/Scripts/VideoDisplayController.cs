using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoDisplayController : MonoBehaviour
{
   
    public RawImage _video;
    public DisplayControl _displayControl;
    //是否更換圖片的依據
    [HideInInspector]
    public bool Change = false;
    //Image resources
    public Texture[] Items;

    //for avoid same image in two screen
    private int HadChoosen;


    Texture randomImage;


    //定時更新圖片用
    float deltaTime = 0f;
    float showTime = 0.1f;

    private void Start()
    {
        //初始時關閉動畫物件，呼叫此物件時順便打開動畫物件
        _video.gameObject.SetActive(true);
        
    }



    void Update()
    {
        //每0.1秒更新圖片
        deltaTime += Time.deltaTime;
        if (deltaTime >= showTime)
        {
            ChangeImage();
            deltaTime = 0f;
        }

            // 收到結束訊號後停止右眼實驗畫面的顯示
        if (!_displayControl._gameStart)
        {
            _video.gameObject.SetActive(false);

        }
    }

    public void ChangeImage()
    {
        Debug.Log("Change items");
        randomImage = GetRandomImage();
        _video.texture = randomImage;
    }

    Texture GetRandomImage()
    {
        // 用do while避免同張圖同時顯示
        // 從Sprites數組中隨機選擇一個Sprite
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, Items.Length - 1);
        }
        while (randomIndex == HadChoosen);

        HadChoosen = randomIndex;
        return Items[randomIndex];
    }
}
