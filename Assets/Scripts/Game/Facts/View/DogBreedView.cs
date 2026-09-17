using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DogBreedView : MonoBehaviour
{
    public event Action<string> OnBreedSelected;

    [SerializeField] private Transform _contentParent;
    [SerializeField] private DogItem _dogItem;
    [SerializeField] private Transform _itemsPlace;
    [SerializeField] private TMP_Text _loadingText;
    
    private List<DogItem> _items = new();
    private DogItem _lastLoadingItem;

    public void ShowLoading(string id)
    {
        if(_lastLoadingItem != null)
            HideLoading(_lastLoadingItem.ID);
        _lastLoadingItem = _items.Find(x => x.ID == id);
        _lastLoadingItem.ShowLoading();        
    }

    public void HideLoading(string id)
    {
        _lastLoadingItem.HideLoading();
        _lastLoadingItem = null;
    }
    
    public void HideLoadingText()
    {
        _loadingText.gameObject.SetActive(false);       
    }
   

    public void AddBreedItem(string id, string name)
    {
        var item = Instantiate(_dogItem, _itemsPlace);
        item.Load(id, name, OnBreedSelected);
        _items.Add(item);
    }
    
    public void Show()
    {
        _loadingText.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        foreach (var item in _items) 
            Destroy(item.gameObject);
        _items.Clear();
    }
}
