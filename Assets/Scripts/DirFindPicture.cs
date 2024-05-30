using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class DirFindPicture : MonoBehaviour
{
    [Header("保存视频文件地址的列表")]
    public List<string> listVideoPath = new List<string>();

    //string path = "\\党建视频\\";
    //exe打包文件的路径
    string configPath = System.Environment.CurrentDirectory + @"\DJmp4";   //视频文件夹

    string ceshimp4 = "E:\\unity.Demo\\Intelligence_Community(zihuixiaoqu)\\智慧小区Demo\\DJmp4";

    public VideoPlayer VideoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        //GetFilesAllMp4(ceshimp4);
        //VideoPlayer.url = listVideoPath[0];

    }
    //获取本地文件夹里所有视频文件
    public void GetFilesAllMp4(string path_)
    {
        if (Directory.Exists(path_))
        {
            DirectoryInfo direction = new DirectoryInfo(path_);
            FileInfo[] files = direction.GetFiles("*");
            Debug.Log("视频数量" + files.Length);
            for (int i = 0; i < files.Length; i++)
            {
                //忽略关联文件
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }
                //Debug.Log("文件名:" + files[i].Name);
                Debug.Log("文件绝对路径:" + files[i].FullName);
                listVideoPath.Add(files[i].FullName);
                //Debug.Log("文件所在目录:" + files[i].DirectoryName);

            }

        }
        else
        {
            return;
        }
    }


    private List<Texture2D> images = new List<Texture2D>();
    private List<Sprite> imageSprite = new List<Sprite>();
    //获取文件夹下所有图片
    public void GetFilesAllSprite(string path_)
    {
        List<string> filePaths = new List<string>();

        string imgtype = "*.BMP|*.JPG|*.GIF|*.PNG";
        string[] ImageType = imgtype.Split('|');

        for (int i = 0; i < ImageType.Length; i++)
        {
            //获取Application.dataPath文件夹下所有的图片路径  
            string[] dirs = Directory.GetFiles((Application.dataPath + path_), ImageType[i]);
            for (int j = 0; j < dirs.Length; j++)
            {
                filePaths.Add(dirs[j]);
            }
        }

        for (int i = 0; i < filePaths.Count; i++)
        {
            Texture2D tx = new Texture2D(100, 100);
            tx.LoadImage(GetImageByte(filePaths[i]));
            //转化成sprite添加到列表使用
            imageSprite.Add(ChangeToSprite(tx));
            //转化成Texture2D添加到列表使用
            images.Add(tx);
        }

    }
    //返回图片的字节流
    private static byte[] GetImageByte(string imagePath)
    {
        FileStream files = new FileStream(imagePath, FileMode.Open);
        byte[] imgByte = new byte[files.Length];
        files.Read(imgByte, 0, imgByte.Length);
        files.Close();
        return imgByte;
    }
    private Sprite ChangeToSprite(Texture2D tex)
    {
        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }

}