using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class VideoDisplayController : MonoBehaviour
{
    public RawImage _video;
    public DisplayControl _displayControl;
    [HideInInspector]
    public bool Change = false;
    public Texture[] Items;
    private int HadChoosen;
    public TMP_InputField _flashFrequence;
    private Texture randomImage;
    private float deltaTime = 0f;
    float showTime ;
    private string imagesFolderPath = Path.Combine(Application.persistentDataPath, "Image", "Mondriands");
    //private string imagesFolderPath = "Assets/Resources/Image/Mondriands";
    

    private void Start()
    {
        _video.gameObject.SetActive(true);
        showTime = 1 / float.Parse(_flashFrequence.text);
        LoadImagesFromFolder(imagesFolderPath);

        if (Items == null || Items.Length == 0)
        {
            Debug.LogError("No images were loaded from the folder.");
        }
    }

    private void LoadImagesFromFolder(string folderPath)
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

    void Update()
    {
        //每showtime秒更新
        deltaTime += Time.deltaTime;
        if (deltaTime >= showTime)
        {
            ChangeImage();
            deltaTime = 0f;
        }

        //收到結束訊號後停止右眼實驗畫面的顯示
        if (!_displayControl._gameStart)
        {
            _video.gameObject.SetActive(false);
        }
    }

    public void ChangeImage()
    {
        if (Items != null && Items.Length > 0)
        {
            Debug.Log("Change items");
            randomImage = GetRandomImage();
            _video.texture = randomImage;
        }
        else
        {
            Debug.LogError("No images available to change.");
        }
    }

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
}
