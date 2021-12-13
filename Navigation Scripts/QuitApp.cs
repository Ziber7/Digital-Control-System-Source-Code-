using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitApp : MonoBehaviour
{
    public GameObject PanelExit;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        if (PanelExit.activeInHierarchy == true)
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex );
        }
        
    }

    public void Exit()
    {
        if (PanelExit.activeInHierarchy == true)
        {
            Application.Quit();
        }
        
    }
}
