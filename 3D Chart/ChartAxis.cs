using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChartAxis : MonoBehaviour
{
    [SerializeField]
    private ObjectPool markPool;
    [SerializeField]
    private ObjectPool lineMarkPool;

    [SerializeField]
    private float labelOffset = 1f;
    [SerializeField]
    private float textSize = 0.1f;
    [SerializeField]
    private string descStr = "Chart";
    [SerializeField]
    private float labelDescOffset = 0.17f;

    private List<PoolableObject> marks = new List<PoolableObject>();
    private List<LineMark> lineMarks = new List<LineMark>();

    private List<string> labels = new List<string>();

    private Label labelDesc;
    private Vector2 size;
    private float gap = 1f;

    private LineRenderer lineRenderer;
    private Vector2 lineOffset;
    private bool useLineMark = false;

    private bool isVertical = false;


    private void Awake()
    {
        
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Start()
    {
    }

    
    void Update()
    {
        
    }

    public void SetMarks(bool vertical, List<string> newLabels, float gap, Vector2 offset, bool useLineMark, Vector2 size)
    {
        this.size = size;
        isVertical = vertical;
        this.lineOffset = offset;
        this.gap = gap;
        this.useLineMark = useLineMark;

        int count = newLabels.Count - labels.Count;
        if (count > 0)
            for (int i = 0; i < count; i++) {
                AddMark();
                if (useLineMark) AddLineMark();
            }
        else if (count < 0)
            for (int i = 0; i < count * -1; i++) {
                RemoveMark();
                if (useLineMark) RemoveLineMark();
            }

        labels = newLabels;

        if (vertical) ArrangeVertical();
        else ArrangeHorizontal();
    }

    private void ArrangeHorizontal()
    {
        for (int i = 0; i < labels.Count; i++)
        {
            PoolableObject mark = marks[i];
            Label label = mark.GetComponent<Label>();
            label.SetLabel(labels[i]);
            label.SetAlign(Label.ALIGN_CENTER);
            label.SetSize(textSize);

            Vector3 newPos = Vector3.right * gap * i;
                
            mark.transform.localPosition = newPos + (Vector3.down * labelOffset); ;

            if (useLineMark)
            {
                LineMark lineMark = lineMarks[i];
                lineMark.transform.localPosition = newPos + (Vector3.down * lineOffset.y);
                lineMark.setOrientation(LineMark.ORIENT_VERTICAL);
            }
        }

        Vector3[] positions = { (Vector3.down * lineOffset.y) + (Vector3.left * lineOffset.x), 
            (Vector3.right * gap * labels.Count) + (Vector3.down * lineOffset.y)};
        lineRenderer.SetPositions(positions);

        SetDesc();
        labelDesc.transform.localPosition = Vector3.right * (gap * labels.Count / 2f) + Vector3.down * labelDescOffset;
    }

    private void ArrangeVertical()
    {
        for (int i = 0; i < labels.Count; i++)
        {
            PoolableObject mark = marks[i];
            Label label = mark.GetComponent<Label>();
            label.SetLabel(labels[i]);
            label.SetAlign(Label.ALIGN_RIGHT);
            label.SetSize(textSize);

            Vector3 newPos = (Vector3.up * gap * i);

            mark.transform.localPosition = newPos + (Vector3.left * labelOffset);

            if (useLineMark)
            {
                LineMark lineMark = lineMarks[i];
                lineMark.transform.localPosition = newPos + (Vector3.left * lineOffset.x);
                lineMark.setOrientation(LineMark.ORIENT_HORIZONTAL);
            }
        }

        Vector3[] positions = { (Vector3.left * lineOffset.x) + (Vector3.down * lineOffset.y), 
            (Vector3.up * gap * labels.Count) + (Vector3.left * lineOffset.x)};
        lineRenderer.SetPositions(positions);

        SetDesc();
        labelDesc.transform.localPosition = Vector3.up * (gap * labels.Count / 2f) + Vector3.left * labelDescOffset;
    }

    private void RemoveMark()
    {
        int index = marks.Count - 1;


        markPool.Return(marks[index]);
        marks[index].SetActive(false);
        marks[index].gameObject.SetActive(false);

        marks.RemoveAt(index);
    }

    private void AddMark()
    {
        PoolableObject go = markPool.GetPooled();
        go.transform.SetParent(transform);
        go.transform.localRotation = Quaternion.identity;
        marks.Add(go);
    }

    private void RemoveLineMark()
    {
        int index = lineMarks.Count - 1;

        lineMarkPool.Return(lineMarks[index].GetComponent<PoolableObject>());
        lineMarks[index].gameObject.SetActive(false);
        lineMarks[index].GetComponent<PoolableObject>().SetActive(false);

        lineMarks.RemoveAt(index);
    }

    private void AddLineMark()
    {
        PoolableObject go = lineMarkPool.GetPooled();
        go.transform.SetParent(transform);
        go.transform.localRotation = Quaternion.identity;
        lineMarks.Add(go.GetComponent<LineMark>());
    }

    private void SetDesc()
    {
        PoolableObject go;
        if (labelDesc == null)
        {
            go = markPool.GetPooled();
            labelDesc = go.GetComponent<Label>();
        }
        else
        {
            go = labelDesc.GetComponent<PoolableObject>();
        }

        go.transform.SetParent(transform);
        go.transform.localRotation = isVertical? Quaternion.AngleAxis(90f, Vector3.forward) : Quaternion.identity;

        //go.transform.localPosition = Vector3.up * (size.y/2);
        labelDesc.SetSize(textSize);

        labelDesc.SetLabel(descStr);
    }
}
