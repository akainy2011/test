using TMPro;
using UnityEngine;
using DG.Tweening;

public class FloatingCurrencyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _moveDistance = 2f;
    [SerializeField] private float _duration = 1f;

    public void Init(string value)
    {
        _text.text = value;
        var startPos = transform.position;

        // Move up and fade out
        transform.DOMove(startPos + Vector3.up * _moveDistance, _duration).SetEase(Ease.OutQuad);

        // Fade out using color alpha
        DOTween.To(
            () => _text.color,
            color => _text.color = color,
            new Color(_text.color.r, _text.color.g, _text.color.b, 0f),
            _duration * 0.5f
        ).SetDelay(_duration * 0.5f);

        // Destroy after animation
        Destroy(gameObject, _duration + 0.1f);
    }
}
