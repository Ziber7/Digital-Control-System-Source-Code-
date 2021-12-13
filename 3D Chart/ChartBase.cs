using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChartBase : MonoBehaviour
{
    [SerializeField]
    protected float textSize = 0.1f;
    [SerializeField]
    protected float titleOffset;
    [SerializeField]
    protected float titleOffsetX;
    [SerializeField]
    private string title;

    [SerializeField]
    private string subtitle;

    [SerializeField]
    protected ObjectPool labelPool;
    [SerializeField]
    protected Vector2 size;

    protected Label labelTitle;
    protected Label labelSubtitle;



    public void SetTitle()
    {
        PoolableObject valueLabel;
        PoolableObject label2;
        if (labelTitle == null)
        {
            valueLabel = labelPool.GetPooled();
            label2 = labelPool.GetPooled();
        }
        else
        {
            valueLabel = labelTitle.GetComponent<PoolableObject>();
            label2 = labelTitle.GetComponent<PoolableObject>();
        }

        valueLabel.transform.SetParent(transform);
        valueLabel.transform.localRotation = Quaternion.identity;
        labelTitle = valueLabel.GetComponent<Label>();

        label2.transform.SetParent(transform);
        label2.transform.localRotation = Quaternion.identity;
        labelSubtitle = label2.GetComponent<Label>();

        valueLabel.transform.localPosition = (Vector3.up * (size.y + titleOffset)) + (Vector3.right * (size.x+titleOffsetX) / 2f);
        labelTitle.SetSize(textSize*1.5f);
        labelTitle.SetLabel(title);
        labelTitle.SetAlign(Label.ALIGN_CENTER);

        label2.transform.localPosition = (Vector3.up * (size.y + titleOffset - 0.04f)) + (Vector3.right * (size.x + titleOffsetX) / 2f);
        labelSubtitle.SetSize(textSize);
        labelSubtitle.SetLabel(subtitle);
        labelSubtitle.SetAlign(Label.ALIGN_CENTER);

    }

    public abstract void SetData(List<ChartDatasetBase> newDataset);

    protected Vector2Int CalcRes(int maxVal)
    {
        int limit;
        int ceil;
        if (maxVal > 100000)
        {
            // 10000
            ceil = (int)Mathf.Ceil(maxVal / 10000);
            limit = (ceil + 1) * 10000;
        }
        else if (maxVal > 10000)
        {
            // 10000
            ceil = (int)Mathf.Ceil(maxVal / 10000);
            limit = (ceil + 1) * 10000;
        }
        else if (maxVal > 1000)
        {
            // 10000
            ceil = (int)Mathf.Ceil(maxVal / 1000);
            limit = (ceil + 1) * 1000;
        }
        else if (maxVal > 500)
        {
            // 1000
            ceil = (int)Mathf.Ceil(maxVal / 100);
            limit = (ceil + 1) * 100;
        }
        else if (maxVal > 100)
        {
            // 500
            ceil = (int)Mathf.Ceil(maxVal / 100);
            limit = (ceil + 1) * 100;
        }
        else if (maxVal > 50)
        {
            // 100
            ceil = (int)Mathf.Ceil(maxVal / 10);
            limit = (ceil + 1) * 10;
        }
        else if (maxVal > 10)
        {
            // 50
            ceil = (int)Mathf.Ceil(maxVal / 10);
            limit = (ceil + 1) * 10;
        }
        else
        {
            // 10
            limit = 10;
            ceil = (int)Mathf.Ceil(maxVal / 10);
        }
        ceil++;
        return new Vector2Int(ceil, limit);
    }
}
