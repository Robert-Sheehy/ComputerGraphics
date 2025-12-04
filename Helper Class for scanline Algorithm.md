
public class RangeX
{
    internal int start = -1;
    internal int end = -1;
    public RangeX() {  }

    public void AddPoint(int x)
    {
        if(start == -1)
        {
            start = x;
            return;
        }
        if(end == -1) 
        {
            if(start <= x)
            {
                end = x;
            }
            else
            {
                end = start;
                start = x;
            }
            return;
        }

        if (x < start)
        {
            start = x;
        }
        if (x > end)
        {
            end = x;
        }
    }
}
