using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFramePresenter : IAnimationFrameProvider
{
    private readonly AnimationFrameModel model;
    private readonly AnimationFrameView view;

    public AnimationFramePresenter(AnimationFrameModel model, AnimationFrameView view)
    {
        this.model = model;
        this.view = view;
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
        model.OnActivateAnimation += view.ActivateAnimation;
    }

    private void DeactivateEvents()
    {
        model.OnActivateAnimation -= view.ActivateAnimation;
    }

    #region Input

    public void ActivateAnimation(string id, Transform target, int cycles = -1)
    {
        model.ActivateAnimation(id, target, cycles);
    }

    #endregion
}

public interface IAnimationFrameProvider
{
    public void ActivateAnimation(string id, Transform target, int cycles = -1);
}
