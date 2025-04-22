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
        _model.Initialize();
        _view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        _model.Dispose();
        _view.Dispose();
    }

    private void ActivateEvents()
    {
        _view.OnChooseActiveTask += _model.ChooseTask;

        _model.OnActivate += _view.Activate;
        _model.OnDeactivate += _view.Deactivate;
        _model.OnSetActivateTask += _view.SetActivateTask;
        _model.OnSetInactivateTask += _view.SetDeactivateTask;
        _model.OnSetCompletedTask += _view.SetCompletedTask;
    }

    private void DeactivateEvents()
    {
        _view.OnChooseActiveTask -= _model.ChooseTask;

        _model.OnActivate -= _view.Activate;
        _model.OnDeactivate -= _view.Deactivate;
        _model.OnSetActivateTask -= _view.SetActivateTask;
        _model.OnSetInactivateTask -= _view.SetDeactivateTask;
        _model.OnSetCompletedTask -= _view.SetCompletedTask;
    }
}
