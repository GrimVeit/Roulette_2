using System;

public class BankPresenter : IMoneyProvider
{
    private readonly BankModel _model;
    private readonly BankView _view;

    public BankPresenter(BankModel model, BankView view)
    {
        this._model = model;
        this._view = view;
    }

    public void Initialize()
    {
        _model.Initialize();
        _view.Initialize();

        _model.OnAddMoney += _view.AddMoney;
        _model.OnRemoveMoney += _view.RemoveMoney;
        _model.OnChangeMoney += _view.SendMoneyDisplay;

        _view.SendMoneyDisplay(_model.Money);
    }

    public void Dispose()
    {
        _model.OnAddMoney -= _view.AddMoney;
        _model.OnRemoveMoney -= _view.RemoveMoney;
        _model.OnChangeMoney -= _view.SendMoneyDisplay;

        _model.Destroy();
    }

    public void SendMoney(int money)
    {
        _model.SendMoney(money);
    }

    public void SendMoney(float money)
    {
        _model.SendMoney(money);
    }

    public bool CanAfford(float bet)
    {
        return _model.CanAfford(bet);
    }

    public float GetMoney() => _model.Money;

    public event Action<float> OnChangeMoney
    {
        add { _model.OnChangeMoney += value; }
        remove { _model.OnChangeMoney -= value; }
    }
}

public interface IMoneyProvider
{
    float GetMoney();

    event Action<float> OnChangeMoney;
    void SendMoney(float money);
    bool CanAfford(float money);
}


