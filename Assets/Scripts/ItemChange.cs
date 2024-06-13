using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemChange : MonoBehaviour
{


    //images[0]: LeftUpper, images[1]:RightUpper, images[2]:LeftLow, images[3]:RightLow
    public RawImage[] _images;
    public RawImage _central;

    public int FamiLocation;

    //是否更換圖片的依據
    [HideInInspector]
    public bool Change = false;
    //Image resources
    public Texture[] familiarItems;
    public Texture[] unfamiliarItems;

    //for avoid same image in two screen
    private int HadChoosen;
    private string familiarImagesFolderPath = Path.Combine(Application.persistentDataPath, "Image", "FamiliarItems");
    private string unfamiliarItemsImagesFolderPath = Path.Combine(Application.persistentDataPath, "Image", "UnfamiliarItems");

    //private string familiarImagesFolderPath = "Assets/Resources/Image/Items";
    //private string unfamiliarItemsImagesFolderPath = "Assets/Resources/Image/Items";


    //決定物品數量
    public TMP_InputField _locationNum;



    Texture randomImage;


    //void LoadImages()
    //{
    //    // 從Resources文件夾中加載所有Sprite
    //    Items = Resources.LoadAll<Texture>(resourcesFolderPath);
    //}
    private void Start()
    {
        LoadImagesFromFolder(familiarImagesFolderPath, ref familiarItems);
        LoadImagesFromFolder(unfamiliarItemsImagesFolderPath, ref unfamiliarItems);

        //決定題數
        if (int.Parse(_locationNum.text) == 1)
        {
            _central.gameObject.SetActive(true);
        }
        else if (int.Parse(_locationNum.text) == 4)
        {
            for(int i = 0; i < 4; i++)
            {
                _images[i].gameObject.SetActive(true);
            }
        }
        
    }



    private void LoadImagesFromFolder(string folderPath, ref Texture[] Items)
    {
        string[] imageTypes = { "*.BMP", "*.JPG", "*.GIF", "*.PNG" };
        List<Texture> texturesList = new List<Texture>();

        if (Directory.Exists(folderPath))
        {
            foreach (string imageType in imageTypes)
            {
                string[] filePaths = Directory.GetFiles(folderPath, imageType);

                foreach (string filePath in filePaths)
                {
                    byte[] fileData = File.ReadAllBytes(filePath);
                    Texture2D texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);
                    texturesList.Add(texture);
                }
            }

            Items = texturesList.ToArray();
        }
        else
        {
            Debug.LogError("Folder path does not exist: " + folderPath);
        }
    }


    //Change two image
    public void ChangeImage()
    {
        if (int.Parse(_locationNum.text) == 1)

            RandomFold_One();
        
        else

            RandomFold_Four();
    }


    Texture GetRandomImage(Texture[] Items)
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

    void RandomFold_Four()
    {
        int randomFold;

        randomFold = Random.Range(0, 3);

        //four location get unfamilarItems
        for (int i = 0; i < 4; i++)
        {
            randomImage = GetRandomImage(unfamiliarItems);
            _images[i].texture = randomImage;
        }

        //select one of them location to get familiarItems
        switch (randomFold)
        {
            case 0:
                FamiLocation = 0;
                randomImage = GetRandomImage(familiarItems);
                _images[0].texture = randomImage;
                break;

            case 1:
                FamiLocation = 1;
                randomImage = GetRandomImage(familiarItems);
                _images[1].texture = randomImage;
                break;

            case 2:
                FamiLocation = 2;
                randomImage = GetRandomImage(familiarItems);
                _images[2].texture = randomImage;
                break;

            case 3:
                FamiLocation = 3;
                randomImage = GetRandomImage(familiarItems);
                _images[3].texture = randomImage;
                break;
        }

    }

    void RandomFold_One()
    {
        
        randomImage = GetRandomImage(familiarItems);
        _central.texture = randomImage;
        
    }

    
}