using System;
public class DogsModel
{
    public event Action OnDogsLoaded;
    public event Action<DogBreedItem> OnBreedDetailLoaded;
    
    private DogBreedItem[] _breeds;
    private DogBreedItem _currentBreedDetail;
    
    public DogBreedItem[] GetBreeds() => _breeds;
    public DogBreedItem GetCurrentBreedDetail() => _currentBreedDetail;
    
    public void SetBreeds(DogBreedItem[] breeds) { _breeds = breeds; OnDogsLoaded?.Invoke(); }
    public void SetBreedDetail(DogBreedItem detail) 
    { 
        _currentBreedDetail = detail; 
        OnBreedDetailLoaded?.Invoke(detail); 
    }
}