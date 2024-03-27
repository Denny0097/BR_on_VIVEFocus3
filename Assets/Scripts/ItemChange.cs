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
    public RawImage Down;

    //是否更換圖片的依據
    [HideInInspector]
    public bool Change = false;
    //Image resources
    public Texture[] Items;

    //for avoid same image in two screen
    private int HadChoosen;


    Texture randomImage;

    // Start is called before the first frame update
    void Start()
    {
        //LoadImages();
        ChangeImage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Change)
        {
            Change = false;
            ChangeImage();
        }

        ////for testing 
        //if (Input.anyKey)
        //{
        //    ChangeImage();
        //}

    }

    //void LoadImages()
    //{
    //    // 從Resources文件夾中加載所有Sprite
    //    Items = Resources.LoadAll<Texture>(resourcesFolderPath);
    //}

    Texture GetRandomImage()
    {
        // 用do while避免同張圖同時顯示
        // 從Sprites數組中隨機選擇一個Sprite
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, Items.Length);
        }
        while (randomIndex == HadChoosen);

        HadChoosen = randomIndex;
        return Items[randomIndex];
    }

    //Change two image
    public void ChangeImage()
    {
        randomImage = GetRandomImage();
        Upper.texture = randomImage;
        randomImage = GetRandomImage();
        Down.texture = randomImage;
    }

}
