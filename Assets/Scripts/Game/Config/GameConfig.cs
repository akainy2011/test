using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Game Config")]
public class GameConfig : ScriptableObject
{
    [Header("Tap Settings")]
    public int TapReward = 1;
    public int TapEnergyCost = 1;

    [Header("Auto Collect Settings")]
    public bool AutoCollectEnabled = true;
    public float AutoCollectInterval = 3f;
    public int AutoCollectReward = 1;
    public int AutoCollectEnergyCost = 1;

    [Header("Energy Settings")]
    public int MaxEnergy = 1000;
    public int EnergyRefillAmount = 10;
    public float EnergyRefillInterval = 10f;

    [Header("VFX Settings")]
    public float FloatingTextDuration = 1f;
    public float FloatingTextMoveSpeed = 2f;
    public float ButtonPressScaleDown = 0.9f;
    public float ButtonPressDuration = 0.1f;

    [Header("UI Settings")]
    public string CurrencyUnit = "Coins";
}
