using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wave.Essence.Eye;
using Wave.XR;


public class FocusEyeData
{
    //Combined Eye:
    public Vector3 CombinedEyeOrigin;
    public Vector3 CombindedEyeDirectionNormalized;

    //Left Eye:
    //
    public Vector3 LeftEyeOrigin;
    //
    public Vector3 LeftEyeDirectionNormalized;
    //睜眼閉眼
    public float LeftEyeOpenness;
    //瞳孔直徑
    public float LeftEyePupilDiameter;
    //瞳孔位置
    public Vector2 LeftEyePupilPositionInSensorArea;

    //Right Eye:
    public Vector3 RightEyeOrigin;
    public Vector3 RightEyeDirectionNormalized;
    public float RightEyeOpenness;
    public float RightEyePupilDiameter;
    public Vector2 RightEyePupilPositionInSensorArea;
}

public class FocusEyeDataManager : MonoBehaviour
{
    public FocusEyeData GetEyeData()
    {
        //Debug.Log("geteyedata");
        bool result = true;
        FocusEyeData data = new FocusEyeData();

        result &= EyeManager.Instance.GetCombinedEyeOrigin(out data.CombinedEyeOrigin);
        result &= EyeManager.Instance.GetCombindedEyeDirectionNormalized(out data.CombindedEyeDirectionNormalized);
        result &= EyeManager.Instance.GetLeftEyeOrigin(out data.LeftEyeOrigin);
        result &= EyeManager.Instance.GetLeftEyeDirectionNormalized(out data.LeftEyeDirectionNormalized);
        result &= EyeManager.Instance.GetLeftEyeOpenness(out data.LeftEyeOpenness);
        result &= EyeManager.Instance.GetLeftEyePupilDiameter(out data.LeftEyePupilDiameter);
        result &= EyeManager.Instance.GetLeftEyePupilPositionInSensorArea(out data.LeftEyePupilPositionInSensorArea);
        result &= EyeManager.Instance.GetRightEyeOrigin(out data.RightEyeOrigin);
        result &= EyeManager.Instance.GetRightEyeDirectionNormalized(out data.RightEyeDirectionNormalized);
        result &= EyeManager.Instance.GetRightEyeOpenness(out data.RightEyeOpenness);
        result &= EyeManager.Instance.GetRightEyePupilDiameter(out data.RightEyePupilDiameter);
        result &= EyeManager.Instance.GetRightEyePupilPositionInSensorArea(out data.RightEyePupilPositionInSensorArea);



        // return result ? data : null;
        return data ;
    }


    //Check if user's eyes are closed over 5s
    public void CheckEyesOpen(FocusEyeData data)
    {
        //確認眼睛是否睜開
        bool IsEyesOpen;

        EyeManager.Instance.GetLeftEyeOpenness(out data.LeftEyeOpenness);
        EyeManager.Instance.GetRightEyeOpenness(out data.RightEyeOpenness);
        if (data.LeftEyeOpenness < 0.5
            && data.RightEyeOpenness < 0.5)
        {
            IsEyesOpen = false;
        }
        else
        {
            IsEyesOpen = true;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (EyeManager.Instance != null)
        {
            EyeManager.Instance.EnableEyeTracking = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
