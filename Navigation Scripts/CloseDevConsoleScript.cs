using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDevConsoleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.developerConsoleVisible = false;
    }  

    void Update()
    {
        Debug.developerConsoleVisible = false;
    }
    
}
