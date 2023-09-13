using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoController : MonoBehaviour
{
    public void INF_SHOW(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void INF_HIDE(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
