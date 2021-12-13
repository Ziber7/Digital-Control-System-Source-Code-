using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssignNamaMesin : MonoBehaviour
{
    string namaMesin;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetString("MesinNama","No Name");
        //TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        namaMesin = PlayerPrefs.GetString("MesinNama");
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.SetText("Machine " + namaMesin);
    }
}
