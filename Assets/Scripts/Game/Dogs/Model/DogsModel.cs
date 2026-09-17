using System;
public class DogsModel
{
    public event Action OnDogsLoaded;
    public event Action<DogBreedItem> OnBreedDetailLoaded;
    
    private DogBreedItem[] _breeds;
    public DogBreedItem[] GetBreeds() => _breeds;

    public void SetBreeds(DogBreedItem[] breeds)
    {
        _breeds = breeds; OnDogsLoaded?.Invoke();
    }
    public void SetBreedDetail(DogBreedItem detail) 
    { 
        OnBreedDetailLoaded?.Invoke(detail); 
    }
}