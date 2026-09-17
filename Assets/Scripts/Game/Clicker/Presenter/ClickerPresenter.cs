using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class ClickerPresenter : BasePresenter
{
    [SerializeField] private ClickerView _view;
    [SerializeField] private CurrencyCounterView _currencyCounterView;
    [SerializeField] private EnergyCounterView _energyCounterView;

    [Inject] private ClickerModel _clickerModel;
    [Inject] private EnergyModel _energyModel;
    [Inject] private GameConfig _config;
    
    private bool _isTabActive;

    public override void Initialize()
    {
        _view.Setup();
        StartAutoCollectLoop();
    }

    public override void Activate()
    {
        _isTabActive = true;
        _view.OnButtonClicked += OnButtonClick;
        _clickerModel.OnCurrencyChanged += OnCurrencyChanged;
        _energyModel.OnEnergyChanged += OnEnergyChanged;
        _currencyCounterView.SetCurrency(_clickerModel.Currency);
        _energyCounterView.SetEnergy(_energyModel.CurrentEnergy, _energyModel.MaxEnergy);
        _view.Show();
    }
    
    public override void Deactivate()
    {
        _view.Hide();
        _isTabActive = false;
        _view.OnButtonClicked -= OnButtonClick;
        _clickerModel.OnCurrencyChanged -= OnCurrencyChanged;
        _energyModel.OnEnergyChanged -= OnEnergyChanged;
    }

    private void OnDestroy()
    {
        Deactivate();
    }

    private void OnButtonClick(bool isAuto)
    {
        PerformClick(isAuto);
    }

    private void PerformClick(bool isAuto)
    {
        var energyCost = isAuto ? _config.AutoCollectEnergyCost : _config.TapEnergyCost;
        var reward = isAuto ? _config.AutoCollectReward : _config.TapReward;

        if (!_energyModel.TrySpendEnergy(energyCost)) 
            return;

        _clickerModel.AddCurrency(reward);
        
        if (_isTabActive)
            _view.TriggerVFX(reward);
    }

    private void OnCurrencyChanged(int currency)
    {
        _currencyCounterView.SetCurrency(currency);
    }

    private void OnEnergyChanged(int energy)
    {
        _energyCounterView.SetEnergy(energy, _energyModel.MaxEnergy);
    }

    private async void StartAutoCollectLoop()
    {
        if (!_config.AutoCollectEnabled) 
            return;

        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_config.AutoCollectInterval));
            PerformClick(true);
        }
    }
}