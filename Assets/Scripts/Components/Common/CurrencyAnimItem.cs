using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CurrencyAnimItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _icon;
    [SerializeField] private float _moveDistance = 300f;
    
    private const float _ANIM_DUR = 0.12f;
    private Sequence _sequence;
    

    public void Init(string value, Action OnComplete)
    {
        _text.text = value;
        var startPosY = transform.position.y;
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 1);
        _icon.color = new Color(_icon.color.r, _icon.color.g, _icon.color.b, 1);
      
        if(_sequence != null)
            _sequence.Kill(true);
        _sequence = DOTween.Sequence();
        _sequence.Insert(0,  transform.DOLocalMoveY(startPosY + _moveDistance, _ANIM_DUR * 15).SetEase(Ease.OutQuad));
        _sequence.Insert(_ANIM_DUR * 5,  _text.DOFade(0,_ANIM_DUR * 10));
        _sequence.Insert(_ANIM_DUR * 5,  _icon.DOFade(0,_ANIM_DUR * 10));
        _sequence.OnComplete(() =>
        {
            _sequence = null;
            OnComplete?.Invoke();
        });
    }
}
