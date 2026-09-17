using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DogBreedPopup : MonoBehaviour
{
    [SerializeField] private RectTransform _bg;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _closeButton;
   

    public void Show(string title, string description)
    {
        _closeButton.onClick.AddListener(Hide);
        _titleText.text = title;
        _descriptionText.text = description;
        gameObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_bg);
    }

    public void Hide()
    {
        _closeButton.onClick.RemoveAllListeners();
        gameObject.SetActive(false);
    }
}
