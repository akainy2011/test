using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private static readonly Vector3 BODY_NORMAL_SCALE = Vector3.one;
    private static readonly Vector3 BODY_DOWN_SCALE = new Vector3(0.95f, 0.95f);

    [SerializeField] private Button _unityButton;
    [SerializeField] private RectTransform _buttonBody;
    [SerializeField] private AudioSource _clickSound;

    private Action _clickHandler;

#if UNITY_EDITOR
    private void Awake()
    {
        if (_unityButton == null)
            throw new Exception($"unityButton property is not set! {name}");

        if (_unityButton.transition != Selectable.Transition.None)
            throw new Exception($"unityButton must have 'none' transition! {name}");

        if (_buttonBody == null)
            throw new Exception($"buttonBody not assigned! {name}");
    }
#endif

    private void Start()
    {
        _unityButton.onClick.AddListener(OnUnityButtonClick);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_unityButton.interactable)
            SetBodyDownState(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_unityButton.interactable)
            SetBodyDownState(false);
    }

    public void AddClickListener(Action listener) => _clickHandler += listener;

    public void RemoveClickListener(Action listener) => _clickHandler -= listener;

    public void RemoveAllListeners() => _clickHandler = null;

    private void OnUnityButtonClick()
    {
        if (Input.touchCount > 1)
            return;
        
        PlaySound();
        _clickHandler?.Invoke();
    }
    
    public  void PlaySound()
    {
        _clickSound.Play();
    }
  

    public void Lock()
    {
        _unityButton.interactable = false;
        SetBodyDownState(false);
    }

    public void Unlock()
    {
        _unityButton.interactable = true;
    }

    private void SetBodyDownState(bool isDown)
    {
        _buttonBody.transform.localScale = isDown ? BODY_DOWN_SCALE : BODY_NORMAL_SCALE;
    }

    private void OnDestroy()
    {
        _clickHandler = null;
    }
  
}
