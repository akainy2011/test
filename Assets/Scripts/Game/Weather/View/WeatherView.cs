using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeatherView : MonoBehaviour
{
    [SerializeField] private Transform _contentParent;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _temperatureText;
    [SerializeField] private Animator _animator;

    public void AddWeatherCard(int temperatureF)
    {
        _animator.Play("Loop");
        _temperatureText.text = $"Сегодня - {temperatureF}F";
    }

    public void SetIcon(Sprite icon)
    {
        _animator.Play("Idle");
        _iconImage.sprite = icon;
    }

    public void PlayIdleAnim()
    {
         _animator.Play("Idle");
    }

    public void Show()
    {
        PlayIdleAnim();
        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);
}