using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Zenject;

public class ClickerView : MonoBehaviour
{
    public event Action<bool> OnButtonClicked; // bool = isAuto

    [Inject] private ObjectPoolManager _poolManager;

    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _buttonRect;
    [SerializeField] private AudioSource _soundSource;

    private ButtonPressVFX _buttonVFX;

    public void Setup()
    {
        _buttonVFX = _buttonRect.GetComponent<ButtonPressVFX>();
        _button.onClick.AddListener(() => OnButtonClicked?.Invoke(false));
    }

    public void TriggerVFX(int reward)
    {
        // 4.1 Particle burst
        SpawnParticles();

        // 4.2 Floating currency text
        SpawnFloatingText(reward);

        // 4.3 Button press animation
        _buttonVFX?.PlayPressAnimation();

        // 4.4 Sound
        _soundSource?.Play();
    }

    private void SpawnParticles()
    {
        var particle = _poolManager.Get<ParticleBurstEffect>();
        if (particle != null)
        {
            particle.transform.position = _buttonRect.transform.position;
            particle.Play();
        }
    }

    private void SpawnFloatingText(int reward)
    {
        var floatingText = _poolManager.Get<FloatingCurrencyText>();
        if (floatingText != null)
        {
            floatingText.transform.position = _buttonRect.transform.position;
            floatingText.Init($"+{reward}");
        }
    }
}
