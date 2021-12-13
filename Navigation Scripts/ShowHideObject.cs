using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideObject : MonoBehaviour
{
    public GameObject Obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowHide()
    {
        if (Obj.activeInHierarchy == false)
        {   
            // Show jika dalam mode hide 
            Obj.SetActive(true);
        } else {
            // Hide Jika dalam mode show
            Obj.SetActive(false);
        }
    }
}
