using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnergyModel
{
    private readonly GameConfig _config;
    private int _currentEnergy;

    public int CurrentEnergy => _currentEnergy;
    public int MaxEnergy => _config.MaxEnergy;
    public event Action<int> OnEnergyChanged;

    public EnergyModel(GameConfig config)
    {
        _config = config;
        _currentEnergy = _config.MaxEnergy;
        OnEnergyChanged?.Invoke(_currentEnergy);
        StartEnergyRefillLoop();
    }

    private async void StartEnergyRefillLoop()
    {
        while (true)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_config.EnergyRefillInterval));
            RefillEnergy();
        }
    }

    private void RefillEnergy()
    {
        _currentEnergy = Mathf.Min(_currentEnergy + _config.EnergyRefillAmount, _config.MaxEnergy);
        OnEnergyChanged?.Invoke(_currentEnergy);
    }

    public bool TrySpendEnergy(int amount)
    {
        if (_currentEnergy >= amount)
        {
            _currentEnergy -= amount;
            OnEnergyChanged?.Invoke(_currentEnergy);
            return true;
        }
        return false;
    }
}
