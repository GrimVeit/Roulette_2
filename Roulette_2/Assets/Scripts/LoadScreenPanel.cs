using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenPanel : MovePanel
{
    [SerializeField] private LazyMotionGroup lazyMotionGroup;

    private void Awake()
    {
        OnDeactivatePanel += lazyMotionGroup.Deactivate;

        Initialize();
    }

    private void OnDestroy()
    {
        OnDeactivatePanel -= lazyMotionGroup.Deactivate;

        Dispose();
    }

    public override void Initialize()
    {
        base.Initialize();

        lazyMotionGroup.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        lazyMotionGroup.Dispose();
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        lazyMotionGroup.Activate();
    }
}
