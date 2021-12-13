
public class ChartDataset3 : ChartDatasetBase
{
    public static int STATE_IDLE = 0;
    public static int STATE_CHANGED = 1;
    public static int STATE_UPDATING = 2;

    public ChartDataset3(string y, int x, int x1)
    {
        this.y = y;
        this.x = x;
        this.x1 = x1;
        this.state = STATE_IDLE;
    }

    public string y
    {
        get;
        set;
    }

    public int x
    {
        get;
        set;
    }

    public int x1
    {
        get;
        set;
    }

    public int state
    {
        get;
        set;
    }
}
