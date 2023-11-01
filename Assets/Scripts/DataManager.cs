using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class DataManager : MonoBehaviour
{
    public FocusEyeDataManager _eyeDataManager;
    
    string _saveDir = "";

    public class LabDataWrapper
    {
        public DateTime time { get; set; } = DateTime.Now;
        public object data { get; set; }
    }

    private void SaveData()
    {
        if (PlayerPrefs.GetInt("GetData") == 1)
        {
            SaveEyeData(_eyeDataManager.GetEyeData());
        }
    }

    public void SaveEyeData(FocusEyeData data)
    {
        string datajson = JsonConvert.SerializeObject(new LabDataWrapper { data = data }, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore, // �� ignore �קK�`���ޥ� (Force �|�����{�h������)
            NullValueHandling = NullValueHandling.Include
        });
        File.AppendAllText(Path.Combine(_saveDir, $"EyeTrackExperiment_EyeData.json"), datajson + "\r\n");
    }

    public void SaveLogMessage(LogMessage data)
    {
        string datajson = JsonConvert.SerializeObject(new LabDataWrapper { data = data }, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore, // �� ignore �קK�`���ޥ� (Force �|�����{�h������)
            NullValueHandling = NullValueHandling.Include
        });
        File.AppendAllText(Path.Combine(_saveDir, $"EyeTrackExperiment_LogMessage.json"), datajson + "\r\n");
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("GetData", 0);
        _saveDir = Path.Combine(Application.persistentDataPath, "Output", DateTime.Now.ToString("yyyyMMdd-hhmmss"));
        Directory.CreateDirectory(_saveDir);
        InvokeRepeating("SaveData", 0f, .01f); //90hz
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}