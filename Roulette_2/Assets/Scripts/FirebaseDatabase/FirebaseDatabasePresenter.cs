using System;

public class FirebaseDatabasePresenter
{
    private readonly FirebaseDatabaseModel _model;
    private readonly FirebaseDatabaseView _view;

    public FirebaseDatabasePresenter(FirebaseDatabaseModel model, FirebaseDatabaseView view)
    {
        _model = model;
        _view = view;
    }

    public void Initialize()
    {
        ActivateEvents();

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
        _model.OnGetUsersRecords += _view.DisplayUsersRecords;
        _model.OnGetNickname += _view.TestDebugNickname;
    }

    private void DeactivateEvents()
    {
        _model.OnGetUsersRecords -= _view.DisplayUsersRecords;
        _model.OnGetNickname -= _view.TestDebugNickname;
    }

    #region Input

    public event Action<UserData> OnGetUserFromPlace
    {
        add { _model.OnGetUserFromPlace += value; }
        remove { _model.OnGetUserFromPlace -= value; }
    }

    public void CreateEmptyDataToServer()
    {
        _model.CreateNewAccountInServer();
    }

    public void SaveChangeToServer()
    {
        _model.SaveChangesToServer();
    }

    public void DisplayUsersRecords()
    {
        _model.DisplayUsersRecords();
    }

    public void SetNickname(string nickname)
    {
        _model.SetNickname(nickname);
    }

    public void SetAvatar(int avatar)
    {
        _model.SetAvatar(avatar);
    }

    public void GetUserFromPlace(int place)
    {
        _model.GetUserFromPlace(place);
    }

    #endregion
}
