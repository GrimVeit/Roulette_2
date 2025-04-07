using System;

public class ChipCounterModel
{
    public event Action<Chip> OnAddChip;
    public event Action<Chip> OnRemoveChip;

    public event Action<int> OnChangeCountChip;

    private int currentCount = 0;

    public void AddChip(Chip chip)
    {
        OnAddChip?.Invoke(chip);

        currentCount += 1;
        OnChangeCountChip?.Invoke(currentCount);
    }

    public void RemoveChip(Chip chip)
    {
        OnRemoveChip?.Invoke(chip);

        currentCount -= 1;
        OnChangeCountChip?.Invoke(currentCount);
    }
}
