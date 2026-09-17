using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class DogBreedsResponse
{
    public List<DogBreedItem> data;
}

[Serializable]
public class DogBreedItem
{
    [SerializeField]  public string id;
    [SerializeField]  public Attributes attributes;

    [Serializable]
    public class Attributes 
    {
        [SerializeField]  public string name;
        [SerializeField]  public string description;
    }
}

[Serializable]
public class DogBreedDetailResponse
{
    public DogBreedItem data;
}

public class GetBreedsRequest : IQueuedRequest<List<DogBreedItem>>
{
    private const string ApiUrl = "https://dogapi.dog/api/v2/breeds";
    private readonly DogsModel _dogsModel;
    public string Id => "GetBreedsRequest";
    UniTask IQueuedRequest.Execute(CancellationToken cancellationToken)
    {
        return Execute(cancellationToken);
    }

    public GetBreedsRequest(DogsModel dogsModel) => _dogsModel = dogsModel;

    public async UniTask<List<DogBreedItem>> Execute(CancellationToken ct = default)
    {
        using var request = UnityWebRequest.Get(ApiUrl);
        request.timeout = 10;
        var operation = request.SendWebRequest();

        while (!operation.isDone && !ct.IsCancellationRequested)
            await UniTask.Yield(cancellationToken: ct);

        if (ct.IsCancellationRequested) { request.Abort(); throw new OperationCanceledException(); }
        if (request.result != UnityWebRequest.Result.Success)
            throw new Exception($"Dog API error: {request.error}");

        var response = JsonUtility.FromJson<DogBreedsResponse>(request.downloadHandler.text);
        var breeds = response?.data ?? new List<DogBreedItem>();
        _dogsModel.SetBreeds(breeds.ToArray());
        return breeds;
    }
}

public class GetBreedRequest : IQueuedRequest<DogBreedItem>
{
    private const string BaseUrl = "https://dogapi.dog/api/v2/breeds";
    private readonly string _breedId;
    private readonly DogsModel _dogsModel;
    public string Id => $"GetBreedRequest_{_breedId}";
    UniTask IQueuedRequest.Execute(CancellationToken cancellationToken)
    {
        return Execute(cancellationToken);
    }

    public GetBreedRequest(string breedId, DogsModel dogsModel)
    {
        _breedId = breedId;
        _dogsModel = dogsModel;
    }

    public async UniTask<DogBreedItem> Execute(System.Threading.CancellationToken ct = default)
    {
        using var request = UnityWebRequest.Get($"{BaseUrl}/{_breedId}");
        request.timeout = 10;
        var operation = request.SendWebRequest();

        while (!operation.isDone && !ct.IsCancellationRequested)
            await UniTask.Yield(cancellationToken: ct);

        if (ct.IsCancellationRequested) { request.Abort(); throw new OperationCanceledException(); }
        if (request.result != UnityWebRequest.Result.Success)
            throw new Exception($"Dog API error: {request.error}");

        var response = JsonUtility.FromJson<DogBreedDetailResponse>(request.downloadHandler.text);
        var detail = response?.data;
        _dogsModel.SetBreedDetail(detail);
        return detail;
    }
}
