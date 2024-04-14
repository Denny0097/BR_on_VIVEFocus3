using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemChange : MonoBehaviour
{

    //Image origin path
    //public string resourcesFolderPath;

    //Up/Down image screen in right eye
    public RawImage Upper;
    public RawImage Lower;

    //是否更換圖片的依據
    [HideInInspector]
    public bool Change = false;
    //Image resources
    public Texture[] Items;

    //for avoid same image in two screen
    private int HadChoosen;


    Texture randomImage;

    //void LoadImages()
    //{
    //    // 從Resources文件夾中加載所有Sprite
    //    Items = Resources.LoadAll<Texture>(resourcesFolderPath);
    //}


    //Change two image
    public void ChangeImage()
    {
        randomImage = GetRandomImage();
        Upper.texture = randomImage;
        randomImage = GetRandomImage();
        Lower.texture = randomImage;
    }


    Texture GetRandomImage()
    {
        // 用do while避免同張圖同時顯示
        // 從Sprites數組中隨機選擇一個Sprite
        int randomIndex;
        do
        {

            randomIndex = Random.Range(0, Items.Length-1);

        }
        while (randomIndex == HadChoosen);

        HadChoosen = randomIndex;
        return Items[randomIndex];
    }
}