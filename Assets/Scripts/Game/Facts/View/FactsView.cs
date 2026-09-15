using UnityEngine;

public class FactsView : MonoBehaviour
{
    [SerializeField] private Transform _contentParent;

    public void Clear()
    {
        foreach (Transform child in _contentParent)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddFactCard(string title, string body)
    {
        // TODO: Instantiate FactCard prefab
        Debug.Log($"[FactsView] Add: {title} - {body}");
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
}
