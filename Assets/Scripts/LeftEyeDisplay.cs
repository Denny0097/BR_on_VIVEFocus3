using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LeftEyeDisplay : MonoBehaviour
{
    public VideoPlayer _video;

    public void PlayVideo()
    {
        _video.Play();
    }

    public void PauseVideo()
    {
        _video.Pause();
    }
}
