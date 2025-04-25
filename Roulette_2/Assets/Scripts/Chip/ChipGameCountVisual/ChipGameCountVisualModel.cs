using System;

public class ChipGameCountVisualModel
{
    public event Action<int, int> OnChangeChipsCount;

    private readonly IStoreChipChangeEvents _storeChipChangeEvents;

    public ChipGameCountVisualModel(IStoreChipChangeEvents storeChipChangeEvents)
    {
        _storeChipChangeEvents = storeChipChangeEvents;

        _storeChipChangeEvents.OnChangeCountChips += ChangeChipsCount;
    }

    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        _storeChipChangeEvents.OnChangeCountChips -= ChangeChipsCount;
    }

    private void ChangeChipsCount(int id, int count)
    {
        OnChangeChipsCount?.Invoke(id, count);
    }
}
