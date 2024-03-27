using UnityEngine;
using UnityEngine.Networking;

public class DisplayContral : MonoBehaviour
{
    public FadeInOut m_Fade;
    public ItemChange _itemChange;

    void Start()
    {
        
            m_Fade.BackGroundControl(false);
       
    }
}