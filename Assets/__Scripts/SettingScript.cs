using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingScript : MonoBehaviour
{
    
    public void Settings(GameObject gameObject)
    {
        
            gameObject.SetActive(true);
    }

    public void Set_HIDE(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
    
}
