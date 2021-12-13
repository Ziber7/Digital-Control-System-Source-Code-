using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColomnChart1 : ChartBase
{
    [SerializeField]
    private ObjectPool colomnPool;

    [SerializeField]
    private ChartAxis axisY;
    [SerializeField]
    private ChartAxis axisX;

    [SerializeField]
    private Vector2 axisOffset;
    [SerializeField]
    private float labelOffset;

    private List<ChartDataset2> dataset = new List<ChartDataset2>();

    private List<PoolableObject> colomnBlocks = new List<PoolableObject>();
    private List<Label> valueLabels = new List<Label>();

    private float xDist = 0.8f;
    private float xScale = 0.06f;

    void Start()
    {
        //StartCoroutine(coroutine());


    }

    void Update()
    {

    }

    IEnumerator coroutine()
    {
        int count = 6;
        while (true)
        {
            yield return new WaitForSeconds(1);
            count = Random.Range(4, 7);
            List<ChartDatasetBase> list = new List<ChartDatasetBase>();

            for (int i = 0; i < count; i++)
            {
                list.Add(new ChartDataset2("aaaaa", Random.Range(0, 500)));
            }
            SetData(list);
        }
    }

    public override void SetData(List<ChartDatasetBase> newDataset)
    {
        SetTitle();

        int count = newDataset.Count - dataset.Count;
        if (count > 0)
            for (int i = 0; i < count; i++) AddBlock();
        if (count < 0)
            for (int i = 0; i < count * -1; i++) RemoveBlock();

        if (count != 0)
        {
            xDist = size.x / newDataset.Count;
        }

        // Fill data
        dataset.Clear();
        newDataset.ForEach(x => dataset.Add((ChartDataset2)x));

        int maxValX = 0;
        for (int i = 0; i < dataset.Count; i++)
        {
            ChartDataset2 data = dataset[i];
            if (data.x > maxValX) maxValX = data.x;
        }
        Vector2Int max = CalcRes(maxValX);

        xScale = size.y / max.y;

        for (int i = 0; i < dataset.Count; i++)
        {
            PoolableObject block = colomnBlocks[i];
            
            ChartDataset2 data = dataset[i];

            Vector3 length = Vector3.up * xScale * data.x;

            Vector3 position = Vector3.right * i * xDist;
            block.transform.localPosition = position;
            block.transform.localScale = Vector3.right + Vector3.forward + length;

            Label label = valueLabels[i];
            label.transform.localPosition = position + length + (Vector3.up * labelOffset);
            label.SetLabel(data.x.ToString());
            label.SetAlign(Label.ALIGN_CENTER);
            label.SetSize(textSize);
        }

        List<string> labelsY = new List<string>();
        dataset.ForEach(x => labelsY.Add(x.y));
        axisX.SetMarks(false, labelsY, xDist, axisOffset, true, size);
        ArrangeAxisY(max);
    }

    private void ArrangeAxisY(Vector2Int ceilLimit)
    {
        float maxVal = ceilLimit.y;
        float length = size.y;
        int res = ceilLimit.x;


        float gap = (maxVal / res) / maxVal * length;

        List<string> labels = new List<string>();

        for (int i = 0; i <= res; i++) labels.Add(Mathf.FloorToInt(maxVal / res * i).ToString());
        axisY.SetMarks(true, labels, gap, axisOffset, true, size);
    }

    private void RemoveBlock()
    {
        int index = colomnBlocks.Count - 1;

        PoolableObject goBlock = colomnBlocks[index];
        colomnPool.Return(goBlock);
        goBlock.SetActive(false);
        goBlock.gameObject.SetActive(false);
        colomnBlocks.RemoveAt(index);

        PoolableObject goLabel = valueLabels[index].GetComponent<PoolableObject>();
        labelPool.Return(goLabel);
        goLabel.SetActive(false);
        goLabel.gameObject.SetActive(false);
        valueLabels.RemoveAt(index);
    }

    private void AddBlock()
    {
        //block
        PoolableObject go = colomnPool.GetPooled();
        go.transform.SetParent(transform);
        go.transform.localRotation = Quaternion.identity;
        colomnBlocks.Add(go);

        // label
        PoolableObject valueLabel = labelPool.GetPooled();
        valueLabel.transform.SetParent(transform);
        valueLabel.transform.localRotation = Quaternion.identity;
        Label label = valueLabel.GetComponent<Label>();
        valueLabels.Add(label);

    }
}
