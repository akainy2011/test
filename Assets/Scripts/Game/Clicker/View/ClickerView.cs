using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ClickerView : MonoBehaviour
{
    public event Action<bool> OnButtonClicked; // bool = isAuto

    [Inject] private ObjectPoolManager _poolManager;

    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _buttonRect;
    [SerializeField] private ParticleBurstEffect _particle;
    [SerializeField] private Transform _animPlace;
    [SerializeField] private CurrencyAnimItem _currencyAnimItem;

    private ButtonPressVFX _buttonVFX;

    public void Setup()
    {
        _buttonVFX = _buttonRect.GetComponent<ButtonPressVFX>();
        _button.onClick.AddListener(() => OnButtonClicked?.Invoke(false));
        _poolManager.Register(_particle, _animPlace, 0);
        _poolManager.Register(_currencyAnimItem, _animPlace, 0);
    }

    public void TriggerVFX(int reward)
    {
        SpawnParticles();
        ShowCurrencyAnimItem(reward);
        _buttonVFX?.PlayPressAnimation();
    }

    private void SpawnParticles()
    {
        var particle = _poolManager.Get<ParticleBurstEffect>();
        if (particle != null)
        {
            particle.transform.position = _buttonRect.transform.position;
            particle.Play();
            ReturnParticleToPool(particle);
        }
    }

    private async UniTaskVoid ReturnParticleToPool(ParticleBurstEffect particle)
    {
        await UniTask.WaitForSeconds(2);
        particle.gameObject.SetActive(false);
        _poolManager.Return(particle);
    }

    private void ShowCurrencyAnimItem(int reward)
    {
        var floatingText = _poolManager.Get<CurrencyAnimItem>();
        if (floatingText != null)
        {
            floatingText.gameObject.SetActive(true);
            floatingText.transform.position = _buttonRect.transform.position;
            floatingText.Init($"+{reward}", OnFlyDone);
        }

        void OnFlyDone()
        {
            floatingText.gameObject.SetActive(false);
            _poolManager.Return(floatingText);
        }
    }
    
    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}