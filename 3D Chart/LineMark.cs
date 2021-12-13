using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMark : MonoBehaviour
{
    public static int ORIENT_HORIZONTAL = 0;
    public static int ORIENT_VERTICAL = 1;

    private LineRenderer lineRenderer;
    private float length = 0.008f;

    public float Length { set => length = value; }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void setOrientation(int orientation)
    {
        Vector3[] pos = new Vector3[2] {
            Vector3.zero, (orientation==ORIENT_HORIZONTAL?Vector3.left:Vector3.down) * length 
        };

        lineRenderer.SetPositions(pos);
    }
}
