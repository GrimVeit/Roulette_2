using System;

public class DailyRewardScaleModel
{
    public event Action<int> OnSetIndex;

    public void SetIndex(int index)
    {
        OnSetIndex?.Invoke(index);
    }
}
