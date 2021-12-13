using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssignIDMesin : MonoBehaviour
{
    string idMesin;
    // Start is called before the first frame update
    void Start()
    {
        //TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        idMesin = PlayerPrefs.GetString("MesinID");
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.SetText("Machine ID : " + idMesin);
    }
}
