using System;

public class StoreTaskPresenter : ITaskProvider
{
    private StoreTaskModel _model;

    public StoreTaskPresenter(StoreTaskModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {
        _model.Dispose();
    }

    #region Input

    public void ActivateTask(string id)
    {
        _model.ActivateTask(id);
    }

    public void CompletedTask(int number)
    {
        _model.CompletedTask(number);
    }

    #endregion

    #region Output

    public event Action<Task> OnInactiveTask
    {
        add => _model.OnInactiveTask += value;
        remove => _model.OnInactiveTask -= value;
    }

    public event Action<Task> OnActiveTask
    {
        add => _model.OnActiveTask += value;
        remove => _model.OnActiveTask -= value;
    }
    public event Action<Task> OnCompletedTask
    {
        add => _model.OnCompletedTask += value;
        remove => _model.OnCompletedTask -= value;
    }

    #endregion
}

public interface ITaskProvider
{
    public void ActivateTask(string id);
}
