using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecordPresenter : IScoreRecordProvider
{
    private readonly ScoreRecordModel _model;

    public ScoreRecordPresenter(ScoreRecordModel model)
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

    public void SetScore(int score)
    {
        _model.SetScore(score);
    }

    #endregion
}

public interface IScoreRecordProvider
{
    public void SetScore(int score);
}
