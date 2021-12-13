using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChartNestedColumn : ChartBase
{
    [SerializeField]
    private ObjectPool colomnPool;

    [SerializeField]
    private ChartAxis axisY;
    [SerializeField]
    private ChartAxisNested axisX;

    [SerializeField]
    private Vector2 axisOffset;
    [SerializeField]
    private float labelOffset;

    [SerializeField]
    private List<Color> colors;

    private List<ChartDataset4> dataset = new List<ChartDataset4>();

    private List<BlockGroup> blockGroups = new List<BlockGroup>();

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
        int count = 4;
        while (true)
        {
            yield return new WaitForSeconds(1);
            count = Random.Range(4, 7);
            List<ChartDatasetBase> list = new List<ChartDatasetBase>();

            for (int i = 0; i < count; i++)
            {
                List<ChartDataset2> contents = new List<ChartDataset2>();
                for (int j = 0; j < Random.Range(1, 4); j++)
                {
                    contents.Add(new ChartDataset2("nn" + j, j * 10 + 30));
                }

                list.Add(new ChartDataset4( "gggg" + i, contents));
            }
            SetData(list);
        }
    }

    public override void SetData(List<ChartDatasetBase> newDataset)
    {
        SetTitle();

        int count = newDataset.Count - dataset.Count;
        if (count > 0)
            for (int i = 0; i < count; i++) AddBlockGroup();
        if (count < 0)
            for (int i = 0; i < count * -1; i++) RemoveBlockGroup();

        // Fill data
        dataset.Clear();
        newDataset.ForEach(x => dataset.Add((ChartDataset4)x));

        int countAllChilds = 0;

        int maxValX = 0;
        for (int i = 0; i < blockGroups.Count; i++)
        {
            BlockGroup group = blockGroups[i];
            ChartDataset4 dataset4 = (ChartDataset4)newDataset[i];

            int maxValCurrent = 0;
            for (int j = 0; j < dataset4.childs.Count; j++)
            {
                countAllChilds++;
                if (dataset4.childs[j].x > maxValCurrent) maxValCurrent = dataset4.childs[j].x;
            }

            if (maxValCurrent > maxValX) maxValX = maxValCurrent;

            TestBlockGroup(group, dataset4);
        }

        Vector2Int max = CalcRes(maxValX);
        xScale = size.y / max.y;

        if (countAllChilds != 0)
        {
            xDist = size.x / countAllChilds;
        }

        int countBlock = 0;
        for (int h = 0; h < dataset.Count; h++)
        {
            BlockGroup group = blockGroups[h];
            ChartDataset4 dataset4 = dataset[h];

            Color color = colors[h % colors.Count];

            for (int i = 0; i < dataset4.childs.Count; i++)
            {
                Label label = group.labels[i];
                ChartBarBlock block = group.blocks[i].GetComponent<ChartBarBlock>();
                ChartDataset2 dataset2 = dataset4.childs[i];

                Vector3 length = Vector3.up * xScale * dataset2.x;

                Vector3 position = Vector3.right * countBlock * xDist;
                countBlock++;

                block.transform.localPosition = position;
                block.transform.localScale = Vector3.right + Vector3.forward + length;
                block.SetColor(color);

                label.transform.localPosition = position + length + (Vector3.up * labelOffset);
                label.SetLabel(dataset2.x.ToString());
                label.SetAlign(Label.ALIGN_CENTER);
                label.SetSize(textSize);
            }
        }

        List<string> labelsY = new List<string>();
        //dataset.ForEach(x => labelsY.Add(x.name));
        axisX.SetMarks(dataset, xDist, axisOffset, false, size);
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

    private void RemoveBlock(BlockGroup group)
    {
        int index = group.blocks.Count - 1;

        PoolableObject goBlock = group.blocks[index];
        colomnPool.Return(goBlock);
        goBlock.SetActive(false);
        goBlock.gameObject.SetActive(false);
        group.blocks.RemoveAt(index);

        PoolableObject goLabel = group.labels[index].GetComponent<PoolableObject>();
        labelPool.Return(goLabel);
        goLabel.SetActive(false);
        goLabel.gameObject.SetActive(false);
        group.labels.RemoveAt(index);
    }

    private void AddBlock(BlockGroup group)
    {
        //block
        PoolableObject go = colomnPool.GetPooled();
        go.transform.SetParent(transform);
        go.transform.localRotation = Quaternion.identity;
        group.blocks.Add(go);

        // label
        PoolableObject valueLabel = labelPool.GetPooled();
        valueLabel.transform.SetParent(transform);
        valueLabel.transform.localRotation = Quaternion.identity;
        Label label = valueLabel.GetComponent<Label>();
        group.labels.Add(label);

    }

    private void TestBlockGroup(BlockGroup group, ChartDataset4 dataset4)
    {
        int count = dataset4.childs.Count - group.blocks.Count;
        if (count > 0)
            for (int i = 0; i < count; i++) AddBlock(group);
        if (count < 0)
            for (int i = 0; i < count * -1; i++) RemoveBlock(group);

        if (count != 0)
        {
            xDist = size.x / dataset4.childs.Count;
        }
    }

    private void RemoveBlockGroup()
    {
        int index = blockGroups.Count - 1;
        BlockGroup group = blockGroups[index];

        group.labels.ForEach(label =>
        {
            PoolableObject goLabel = label.GetComponent<PoolableObject>();
            labelPool.Return(goLabel);
            goLabel.SetActive(false);
            goLabel.gameObject.SetActive(false);
        });
        group.labels.Clear();

        group.blocks.ForEach(goBlock =>
        {
            colomnPool.Return(goBlock);
            goBlock.SetActive(false);
            goBlock.gameObject.SetActive(false);
        });
        group.blocks.Clear();

        blockGroups.RemoveAt(index);
    }

    private void AddBlockGroup()
    {
        blockGroups.Add(new BlockGroup(new List<PoolableObject>(), new List<Label>()));
    }
}
