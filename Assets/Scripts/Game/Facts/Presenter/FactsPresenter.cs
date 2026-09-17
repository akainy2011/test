using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class FactsPresenter : MonoBehaviour, IInitializable
{
    [SerializeField] private FactsView _view;

    [Inject] private RequestQueue _requestQueue;
    [Inject] private FactsModel _factsModel;
    [Inject] private StubFactsRequest _stubFactsRequest;

    public void Initialize()
    {
        _factsModel.OnDataLoaded += OnFactsDataLoaded;
        LoadFactsData();
    }

    private void OnDestroy()
    {
        _factsModel.OnDataLoaded -= OnFactsDataLoaded;
    }

    private void LoadFactsData()
    {
        _requestQueue.Enqueue(_stubFactsRequest);
    }

    private void OnFactsDataLoaded()
    {
        _view.Clear();
        foreach (var data in _factsModel.GetData())
        {
            _view.AddFactCard(data.Title, data.Body);
        }
        _view.Show();
    }
}