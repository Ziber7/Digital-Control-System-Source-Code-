using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartBarBlock : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private bool isColomn = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color color)
    {
        meshRenderer.material.color = color;
    }

    public void SetScale(float wide)
    {
        if(isColomn)
            transform.localScale = (Vector3.right * wide) + (Vector3.forward * wide * 0.5f);
        else
            transform.localScale = (Vector3.up * wide) + (Vector3.forward * wide * 0.5f);
    }

    public void SetLength(float length) 
    {
        transform.localScale = transform.localScale;
    }

    public void SetPos(Vector3 pos)
    {

    }
}
