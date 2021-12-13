using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class ChartDataset4 : ChartDatasetBase
{

    public List<ChartDataset2> childs;
    public string name;

    public ChartDataset4(string name ,List<ChartDataset2> childs)
    {
        this.childs = childs;
        this.name = name;
    }
}