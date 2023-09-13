using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    [Header("Tags")]
    [SerializeField] private string createdTag;

    // Start is called before the first frame update
    private void Awake()
    {
        GameObject obj = GameObject.FindWithTag(this.createdTag);
        if(obj != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.tag = this.createdTag;
            DontDestroyOnLoad(this.gameObject);
        }
    }


}
