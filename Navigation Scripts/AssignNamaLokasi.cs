using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AssignNamaLokasi : MonoBehaviour
{
    string namaLokasi;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetString("LokasiNama","No Name");
        //TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        namaLokasi = PlayerPrefs.GetString("LokasiNama");
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.SetText("Location " + namaLokasi);
    }
}
