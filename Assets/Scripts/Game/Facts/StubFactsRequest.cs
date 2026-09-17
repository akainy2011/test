using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

// Stub facts request for demonstration
public class StubFactsRequest : IQueuedRequest<List<FactData>>
{
    private readonly FactsModel _factsModel;

    public string Id => "StubFactsRequest";

    public StubFactsRequest(FactsModel factsModel)
    {
        _factsModel = factsModel;
    }

    public UniTask<List<FactData>> Execute(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    UniTask IQueuedRequest.Execute(CancellationToken cancellationToken)
    {
        return Execute(cancellationToken);
    }
    
    public async UniTask<List<FactData>> Execute()
    {
        // Simulate network delay
        await UniTask.Delay(TimeSpan.FromSeconds(1));

        var data = new List<FactData>
        {
            new FactData { Title = "Space", Body = "Space is completely silent. No sound can travel through a vacuum." },
            new FactData { Title = "Octopus", Body = "Octopuses have three hearts and blue blood." },
            new FactData { Title = "Honey", Body = "Honey never spoils. Archaeologists have found 3000-year-old honey that was still edible." },
        };

        // Set data to model
        _factsModel.SetData(data.ToArray());

        return data;
    }
}