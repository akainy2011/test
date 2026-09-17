using UnityEngine;
using Zenject;

public class DogBreedPresenter : BasePresenter
{
    [SerializeField] private DogBreedView _view;
    [SerializeField] private DogBreedPopup _popup;

    [Inject] private RequestQueue _requestQueue;
    [Inject] private DogsModel _dogsModel;
    [Inject] private GetBreedsRequest _getBreedsRequest;

    private bool _isTabActive;
    
    public override void Activate()
    {
        _view.Show();
        _dogsModel.OnDogsLoaded += OnDogsLoaded;
        _dogsModel.OnBreedDetailLoaded += OnBreedDetailLoaded;
        _view.OnBreedSelected += OnBreedSelected;
        _isTabActive = true;
        LoadBreeds();
        
    }
    
    public override void Deactivate()
    {
        _view.Hide();
        _popup.Hide();
        _isTabActive = false;
        _dogsModel.OnDogsLoaded -= OnDogsLoaded;
        _dogsModel.OnBreedDetailLoaded -= OnBreedDetailLoaded;
        _view.OnBreedSelected -= OnBreedSelected;
        _requestQueue.CancelAllOfType<GetBreedRequest>();
        _requestQueue.CancelAllOfType<GetBreedsRequest>();
    }

    private void OnDestroy()
    {
        Deactivate();
    }

    private void LoadBreeds()
    {
        _requestQueue.Enqueue(_getBreedsRequest,
            onError: ex => Debug.LogError($"Failed to load breeds: {ex.Message}"));
    }

    private void OnDogsLoaded()
    {
        _view.HideLoadingText();
        var breeds = _dogsModel.GetBreeds();
        for (int i = 0; i < breeds.Length && i < 10; i++)
            _view.AddBreedItem( breeds[i].id, breeds[i].attributes.name);
    }

    private void OnBreedSelected(string id)
    {
        _requestQueue.CancelAllOfType<GetBreedRequest>();
        _view.ShowLoading(id);
        var request = new GetBreedRequest(id, _dogsModel);
        _requestQueue.Enqueue(request,
            onError: ex => Debug.LogError($"Failed to load breed: {ex.Message}"));
    }

    private void OnBreedDetailLoaded(DogBreedItem detail)
    {
        _view.HideLoading(detail.id);
        _popup.Show(detail.attributes.name, detail.attributes.description);
    }
}
