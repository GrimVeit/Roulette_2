using System;

public class StoreTaskPresenter : ITaskProviderEvents, IActivateTaskProvider, ICompleteTaskProvider, ITaskCompletedProviderEvent
{
    private readonly StoreTaskModel _model;

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

    public void ChangeTasks()
    {
        _model.ChangeTasks();
    }

    #endregion

    #region Output

    public event Action<Task> OnActivate
    {
        add => _model.OnActivate += value;
        remove => _model.OnActivate -= value;
    }

    public event Action<Task> OnDeactivate
    {
        add => _model.OnDeactivate += value;
        remove => _model.OnDeactivate -= value;
    }



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

    public event Action<Task> OnComplete
    {
        add => _model.OnComplete += value;
        remove => _model.OnComplete -= value;
    }

    #endregion
}

public interface ITaskProviderEvents
{
    public event Action<Task> OnActivate;
    public event Action<Task> OnDeactivate;

    public event Action<Task> OnInactiveTask;
    public event Action<Task> OnActiveTask;
    public event Action<Task> OnCompletedTask;
}

public interface ITaskCompletedProviderEvent
{
    public event Action<Task> OnComplete;
}

public interface IActivateTaskProvider
{
    public void ActivateTask(string id);
}

public interface ICompleteTaskProvider
{
    public void CompletedTask(int id);
}
