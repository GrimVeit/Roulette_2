using System;

public class TaskVisualPresenter
{
    private readonly TaskVisualModel _model;
    private readonly TaskVisualView _view;

    public TaskVisualPresenter(TaskVisualModel model, TaskVisualView view)
    {
        _model = model;
        _view = view;

        ActivateEvents();
    }

    public void Initialize()
    {
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseActiveTask += _model.ChooseTask;

        _model.OnSetActivateTask += _view.SetActivateTask;
        _model.OnSetInactivateTask += _view.SetDeactivateTask;
        _model.OnSetCompletedTask += _view.SetCompletedTask;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseActiveTask -= _model.ChooseTask;

        _model.OnSetActivateTask -= _view.SetActivateTask;
        _model.OnSetInactivateTask -= _view.SetDeactivateTask;
        _model.OnSetCompletedTask -= _view.SetCompletedTask;
    }

    #region Input

    public void SetActivateTask(Task task)
    {
        _model.SetActivateTask(task);
    }

    public void SetInactivateTask(Task task)
    {
        _model.SetInactivaTask(task);
    }

    public void SetCompletedTask(Task task)
    {
        _model.SetCompletedTask(task);
    }

    #endregion

    #region Otput

    public event Action<int> OnChooseTask
    {
        add => _model.OnChooseTask += value;
        remove => _model.OnChooseTask -= value;
    }

    #endregion
}
