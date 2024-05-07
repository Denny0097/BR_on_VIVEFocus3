using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CameraChange : MonoBehaviour
{
    public Camera _rightEye;
    public Camera _leftEye;
    public Text Btntext;

    


    public void ChangeDispplay()
    {

        if (_leftEye.cullingMask == (1 << 0) + (1 << 5) + (1 << 6))
        {
            Btntext.text = "Left eye  Mondrians";
            _rightEye.cullingMask = (1 << 0) + (1 << 5) + (1 << 6);
            _leftEye.cullingMask = (1 << 0) + (1 << 5) + (1 << 7);
        }
        else if (_leftEye.cullingMask == (1 << 0) + (1 << 5) + (1 << 7))
        {
            Btntext.text = "Right eye  Mondrians";
            _leftEye.cullingMask = (1 << 0) + (1 << 5) + (1 << 6); 
            _rightEye.cullingMask = (1 << 0) + (1 << 5) + (1 << 7);
        }
    }
    
}
