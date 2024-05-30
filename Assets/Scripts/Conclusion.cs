using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Conclusion : MonoBehaviour
{
    public TMP_InputField _inputfield;


    public void Add()
    {
        int rawNumber = int.Parse(_inputfield.text);
        rawNumber++;
        _inputfield.text = rawNumber.ToString();
    }

    public void Minus()
    {
        
        int rawNumber = int.Parse(_inputfield.text);
        if(rawNumber >0)
            rawNumber--;
        _inputfield.text = rawNumber.ToString();

    }

    public void AddTen()
    {
        int rawNumber = int.Parse(_inputfield.text);
        rawNumber += 10;
        _inputfield.text = rawNumber.ToString();
    }

    public void MinusTen()
    {

        int rawNumber = int.Parse(_inputfield.text);
        if (rawNumber > 0)
            rawNumber -= 10;
        _inputfield.text = rawNumber.ToString();

    }
}
