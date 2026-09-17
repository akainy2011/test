using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DogItem : MonoBehaviour
{ 
    [SerializeField] private GameObject _loadingIndicator;
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Button _button;

    private string _id;

    public void Load(string id, string name, Action<string> OnBreedSelected)
    {
        _id = id;
        _label.text = name;
        _button.onClick.AddListener(() => OnBreedSelected?.Invoke(id));
        _loadingIndicator?.SetActive(false);
    }
    
    public string ID => _id;

    public void ShowLoading()
    {
        _loadingIndicator?.SetActive(true);
    }

    public void HideLoading()
    {
        _loadingIndicator?.SetActive(false);
    }
}
