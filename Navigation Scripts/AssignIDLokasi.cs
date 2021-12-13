using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssignIDLokasi : MonoBehaviour
{
    string idLokasi;
    // Start is called before the first frame update
    void Start()
    {
        //TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        idLokasi = PlayerPrefs.GetString("LokasiID");
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.SetText("Location ID : " + idLokasi);
    }
}
