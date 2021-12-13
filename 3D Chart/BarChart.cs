using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarChart : ChartBase
{
    [SerializeField]
    private ObjectPool barPool;

    [SerializeField]
    private ChartAxis axisY;
    [SerializeField]
    private ChartAxis axisX;

    [SerializeField]
    private Vector2 axisOffset;
    [SerializeField]
    private float labelOffset;

    private List<ChartDataset2> dataset = new List<ChartDataset2>();

    private List<PoolableObject> barBlocks = new List<PoolableObject>();
    private List<Label> valueLabels = new List<Label>();

    private float yDist = 0.8f;
    private float xScale = 0.06f;

    void Start()
    {
    }

    public override void SetData(List<ChartDatasetBase> newDataset)
    {
        SetTitle();

        int count = newDataset.Count - dataset.Count;
        if (count > 0)
            for (int i = 0; i < count; i++) AddBlock();
        if (count < 0)
            for (int i = 0; i < count * -1; i++) RemoveBlock();

        if(count != 0)
        {
            yDist = size.y/newDataset.Count;
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

        xScale = size.x / max.y;

        for (int i = 0; i < dataset.Count; i++)
        {
            PoolableObject block = barBlocks[i];
            ChartDataset2 data = dataset[i];

            Vector3 position = Vector3.up * i * yDist;
            block.transform.localPosition = position;
            Vector3 length = Vector3.right * xScale * data.x;
            block.transform.localScale = Vector3.up + Vector3.forward + length;

            Label label = valueLabels[i];
            label.transform.localPosition = position + length + (Vector3.right * labelOffset);
            label.SetLabel(data.x.ToString());
            label.SetAlign(Label.ALIGN_LEFT);
            label.SetSize(textSize);
        }

        List<string> labelsY = new List<string>();
        dataset.ForEach(x => labelsY.Add(x.y));

        axisY.SetMarks(true, labelsY, yDist, axisOffset, true, size);

        ArrangeAxisX(max);
    }

    private void ArrangeAxisX(Vector2Int ceilLimit)
    {
        float maxVal = ceilLimit.y;
        float length = size.x;
        int res = ceilLimit.x;


        float gap = (maxVal / res) / maxVal * length;

        List<string> labels = new List<string>();

        for (int i = 0; i <= res; i++) labels.Add(Mathf.FloorToInt(maxVal/res*i).ToString());

        axisX.SetMarks(false, labels, gap, axisOffset, true, size);
    }

    private void RemoveBlock()
    {
        int index = barBlocks.Count - 1;
        
        PoolableObject goBlock = barBlocks[index];
        barPool.Return(goBlock);
        goBlock.SetActive(false);
        goBlock.gameObject.SetActive(false);
        barBlocks.RemoveAt(index);

        PoolableObject goLabel = valueLabels[index].GetComponent<PoolableObject>();
        labelPool.Return(goLabel);
        goLabel.SetActive(false);
        goLabel.gameObject.SetActive(false);
        valueLabels.RemoveAt(index);
    }

    private void AddBlock()
    {   
        //block
        PoolableObject go = barPool.GetPooled();
        go.transform.SetParent(transform);
        go.transform.localRotation = Quaternion.identity;
        barBlocks.Add(go);

        // label
        PoolableObject valueLabel = labelPool.GetPooled();
        valueLabel.transform.SetParent(transform);
        valueLabel.transform.localRotation = Quaternion.identity;
        Label label = valueLabel.GetComponent<Label>();
        valueLabels.Add(label);
        
    }


}
