using UnityEngine;
using DG.Tweening;

public class ButtonPressVFX : MonoBehaviour
{
    [SerializeField] private float _scaleDown = 0.9f;
    [SerializeField] private float _pressDuration = 0.1f;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void PlayPressAnimation()
    {
        var currentScale = _rectTransform.localScale;
        var pressedScale = currentScale * _scaleDown;

        DOTween.To(() => _rectTransform.localScale, x => _rectTransform.localScale = x,
            pressedScale, _pressDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                DOTween.To(() => _rectTransform.localScale, x => _rectTransform.localScale = x,
                    currentScale, _pressDuration * 1.5f)
                    .SetEase(Ease.OutBack);
            });
    }
}
