using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveInputFields : MonoBehaviour
{
    // References to TMP_InputFields
    public TMP_InputField _inputQuizNum;
    public TMP_InputField _inputRoundTime;
    public TMP_InputField _locationNum;
    public TMP_InputField _flashFrequence;

    // Keys for PlayerPrefs
    private const string InputQuizNumKey = "InputQuizNum";
    private const string InputRoundTimeKey = "InputRoundTime";
    private const string LocationNumKey = "LocationNum";
    private const string FlashFrequenceKey = "FlashFrequence";

    void Start()
    {
        // Load the saved input field content if it exists
        if (PlayerPrefs.HasKey(InputQuizNumKey))
        {
            _inputQuizNum.text = PlayerPrefs.GetString(InputQuizNumKey);
        }
        if (PlayerPrefs.HasKey(InputRoundTimeKey))
        {
            _inputRoundTime.text = PlayerPrefs.GetString(InputRoundTimeKey);
        }
        if (PlayerPrefs.HasKey(LocationNumKey))
        {
            _locationNum.text = PlayerPrefs.GetString(LocationNumKey);
        }
        if (PlayerPrefs.HasKey(FlashFrequenceKey))
        {
            _flashFrequence.text = PlayerPrefs.GetString(FlashFrequenceKey);
        }

        // Add listeners to save the input field content whenever it changes
        _inputQuizNum.onValueChanged.AddListener(SaveInputQuizNumContent);
        _inputRoundTime.onValueChanged.AddListener(SaveInputRoundTimeContent);
        _locationNum.onValueChanged.AddListener(SaveLocationNumContent);
        _flashFrequence.onValueChanged.AddListener(SaveFlashFrequenceContent);
    }

    void SaveInputQuizNumContent(string input)
    {
        // Save the input field content to PlayerPrefs
        PlayerPrefs.SetString(InputQuizNumKey, input);
        PlayerPrefs.Save();
    }

    void SaveInputRoundTimeContent(string input)
    {
        PlayerPrefs.SetString(InputRoundTimeKey, input);
        PlayerPrefs.Save();
    }

    void SaveLocationNumContent(string input)
    {
        PlayerPrefs.SetString(LocationNumKey, input);
        PlayerPrefs.Save();
    }

    void SaveFlashFrequenceContent(string input)
    {
        PlayerPrefs.SetString(FlashFrequenceKey, input);
        PlayerPrefs.Save();
    }
}
