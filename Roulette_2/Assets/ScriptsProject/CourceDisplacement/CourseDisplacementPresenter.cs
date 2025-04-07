using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourseDisplacementPresenter
{
    private readonly CourseDisplacementModel _model;
    private readonly CourseDisplacementView _view;

    public CourseDisplacementPresenter(CourseDisplacementModel model, CourseDisplacementView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();
    }

    public void Dispose()
    {
        DeactivateEvents();
    }

    private void ActivateEvents()
    {
        _model.OnChangeCourse += _view.SetCourse;
    }

    private void DeactivateEvents()
    {
        _model.OnChangeCourse -= _view.SetCourse;
    }

    #region Input

    public void Left(int courseRoute)
    {
        _model.Left(courseRoute);
    }

    public void Right(int courseRoute)
    {
        _model.Right(courseRoute);
    }

    public void Clear()
    {
        _model.Clear();
    }

    #endregion
}


