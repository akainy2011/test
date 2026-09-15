using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

// Stub facts request for demonstration
public class StubFactsRequest : IQueuedRequest<List<FactData>>
{
    public string Id => "StubFactsRequest";

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
        var factsModel = UnityEngine.Object.FindObjectOfType<FactsModel>();
        factsModel?.SetData(data.ToArray());

        return data;
    }
}
