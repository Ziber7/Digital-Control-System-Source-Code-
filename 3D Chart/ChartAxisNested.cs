using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class ChartAxisNested : MonoBehaviour
{
    [SerializeField]
    private ObjectPool markPool;
    [SerializeField]
    private ObjectPool lineMarkPool;

    [SerializeField]
    private float labelOffset = 0.1f;
    [SerializeField]
    private float labelOffset2 = 0.05f;
    [SerializeField]
    private float textSize = 0.1f;
    [SerializeField]
    private string descStr = "Chart";
    [SerializeField]
    private float labelDescOffset = 0.1f;
    [SerializeField]
    private List<Color> colors;

    private Label labelDesc;

    private List<LabelGroup> labelGroup = new List<LabelGroup>();

    private List<ChartDataset4> datasets = new List<ChartDataset4>();

    private float gap = 1f;

    private LineRenderer lineRenderer;
    private Vector2 lineOffset;
    private bool useLineMark = false;
    private Vector2 size;


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

    public void SetMarks(List<ChartDataset4> newDataset, float gap, Vector2 offset, bool useLineMark, Vector2 size)
    {
        this.size = size;
        this.lineOffset = offset;
        this.gap = gap;
        this.useLineMark = useLineMark;

        datasets.Clear();
        newDataset.ForEach(dataset => datasets.Add(dataset));

        int count = newDataset.Count - labelGroup.Count;
        if (count > 0)
            for (int i = 0; i < count; i++)
            {
                AddLabelGroup();
            }
        else if (count < 0)
            for (int i = 0; i < count * -1; i++)
            {
                RemoveLabelGroup();
            }

        for (int i = 0; i < newDataset.Count; i++)
        {
            TestLabelGroup(labelGroup[i], datasets[i]);
        }

        int index = 0;
        for (int i = 0; i < labelGroup.Count; i++)
        {
            LabelGroup group = labelGroup[i];
            ChartDataset4 dataset4 = datasets[i];
            Label label = group.label;
            label.SetLabel(dataset4.name);
            label.SetAlign(Label.ALIGN_CENTER);
            label.SetSize(textSize);

            int colorIndex = i % colors.Count;
            label.SetColor(colors[colorIndex]);

            float min = 0;
            float max = 0;
            for (int j = 0; j < group.labels.Count; j++)
            {
                Vector3 newPos = Vector3.right * gap * index;

                if (j == 0) min = newPos.x;
                max = newPos.x;

                Label label2 = group.labels[j];
                label2.SetLabel(dataset4.childs[j].y);
                label2.SetColor(colors[colorIndex]);
                label2.SetAlign(Label.ALIGN_CENTER);
                label2.SetSize(textSize);

                label2.transform.localPosition = newPos + (Vector3.down * labelOffset); ;

                index++;
            }

            float center = (max - min) / 2 + min;
            label.transform.localPosition = (Vector3.right * center) + (Vector3.down * (labelOffset2 + labelOffset));
        }

        Vector3[] positions = { (Vector3.down * lineOffset.y) + (Vector3.left * lineOffset.x),
            (Vector3.right * gap * index) + (Vector3.down * lineOffset.y)};
        lineRenderer.SetPositions(positions);
        SetDesc();
        labelDesc.gameObject.transform.localPosition = Vector3.right * (gap * index / 2f) + (Vector3.down * labelDescOffset);
    }

    private void TestLabelGroup(LabelGroup group, ChartDataset4 dataset4)
    {
        int count = dataset4.childs.Count - group.labels.Count;
        if (count > 0)
            for (int i = 0; i < count; i++)
            {
                AddLabel(group);
            }
        else if (count < 0)
            for (int i = 0; i < count * -1; i++)
            {
                RemoveLabel(group);
            }
    }

    private void RemoveLabel(LabelGroup group)
    {

        List<Label> labels = group.labels;
        int index = labels.Count - 1;
        PoolableObject label = labels[index].GetComponent<PoolableObject>();
        group.labels[index].SetColor(Color.white);
        markPool.Return(label);
        label.SetActive(false);
        label.gameObject.SetActive(false);
        labels.RemoveAt(index);
    }

    private void AddLabel(LabelGroup group)
    {
        PoolableObject go = markPool.GetPooled();
        go.transform.SetParent(transform);
        go.transform.localRotation = Quaternion.identity;
        group.labels.Add(go.GetComponent<Label>());
    }

    private void RemoveLabelGroup()
    {
        int index = labelGroup.Count - 1;

        LabelGroup group = labelGroup[index];

        for (int i = 0; i < group.labels.Count; i++)
        {
            PoolableObject poolableObject = group.labels[i].GetComponent<PoolableObject>();
            group.labels[i].SetColor(Color.white);
            markPool.Return(poolableObject);
            poolableObject.SetActive(false);
            poolableObject.gameObject.SetActive(false);
        }

        group.labels.Clear();


        // 

        PoolableObject labelMain = group.label.GetComponent<PoolableObject>();
        group.label.SetColor(Color.white);
        markPool.Return(labelMain);
        labelMain.SetActive(false);
        labelMain.gameObject.SetActive(false);


        labelGroup.RemoveAt(index);
    }

    private void AddLabelGroup()
    {
        PoolableObject go = markPool.GetPooled();
        go.transform.SetParent(transform);
        go.transform.localRotation = Quaternion.identity;

        labelGroup.Add(new LabelGroup(new List<Label>(), go.GetComponent<Label>()));
    }

    private void SetDesc()
    {
        PoolableObject go;
        if (labelDesc == null)
        {
            Debug.Log("ok");
            go = markPool.GetPooled();
            labelDesc = go.GetComponent<Label>();
        }
        else
        {
            Debug.Log("stop");
            go = labelDesc.GetComponent<PoolableObject>();
        }

        go.transform.SetParent(transform);
        go.transform.localRotation = Quaternion.identity;
        //go.transform.localPosition = Vector3.up * (size.y*10 / 2);

        labelDesc.SetSize(textSize);
        labelDesc.SetLabel(descStr);
    }

}