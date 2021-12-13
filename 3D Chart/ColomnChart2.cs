using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColomnChart2 : ChartBase                                                                                                                   
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

    private List<ChartDataset3> dataset = new List<ChartDataset3>();

    private List<PoolableObject> colomnBlocks = new List<PoolableObject>();
    private List<PoolableObject> colomnDistTarget = new List<PoolableObject>();
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
                list.Add(new ChartDataset3("aaaaa", Random.Range(0, 500), Random.Range(0, 500)));
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
        newDataset.ForEach(x => dataset.Add((ChartDataset3)x));

        int maxValX = 0;
        for (int i = 0; i < dataset.Count; i++)
        {
            ChartDataset3 data = dataset[i];
            if (data.x > maxValX) maxValX = data.x;
            if (data.x1 > maxValX) maxValX = data.x1;
        }
        Vector2Int max = CalcRes(maxValX);

        xScale = size.y / max.y;

        for (int i = 0; i < dataset.Count; i++)
        {
            PoolableObject block = colomnBlocks[i];
            PoolableObject block1 = colomnDistTarget[i];

            ChartBarBlock blockDist = block1.GetComponent<ChartBarBlock>();

            ChartDataset3 data = dataset[i];

            float dist2Tar = data.x1 - data.x;
            bool above = false;
            if (dist2Tar > 0) above = true;

            if (!above) blockDist.SetColor(Color.green);
            else blockDist.SetColor(Color.red);

            Vector3 length = Vector3.up * xScale * (dist2Tar < 0 ? data.x + dist2Tar : data.x);
            Vector3 length1 = Vector3.up * xScale * Mathf.Abs(dist2Tar);

            Vector3 position = Vector3.right * i * xDist;
            block.transform.localPosition = position;
            block.transform.localScale = Vector3.right + Vector3.forward + length;

            block1.transform.localPosition = position + length;
            block1.transform.localScale = Vector3.right + Vector3.forward + length1;

            Label label = valueLabels[i];
            label.transform.localPosition = position + length + length1 + (Vector3.up * labelOffset);
            label.SetLabel(data.x.ToString() + "/" + data.x1.ToString());
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
        int index1 = colomnDistTarget.Count - 1;

        PoolableObject goBlock = colomnBlocks[index];
        colomnPool.Return(goBlock);
        goBlock.SetActive(false);
        goBlock.gameObject.SetActive(false);
        colomnBlocks.RemoveAt(index);

        PoolableObject goBlock1 = colomnDistTarget[index];
        colomnPool.Return(goBlock1);
        goBlock1.SetActive(false);
        goBlock1.gameObject.SetActive(false);
        colomnDistTarget.Remove(goBlock1);

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

        //block2
        PoolableObject go1 = colomnPool.GetPooled();
        go1.transform.SetParent(transform);
        go1.transform.localRotation = Quaternion.identity;
        colomnDistTarget.Add(go1);

        // label
        PoolableObject valueLabel = labelPool.GetPooled();
        valueLabel.transform.SetParent(transform);
        valueLabel.transform.localRotation = Quaternion.identity;
        Label label = valueLabel.GetComponent<Label>();
        valueLabels.Add(label);

    }
}
