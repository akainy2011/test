using UnityEngine;

public class ParticleBurstEffect : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void Play()
    {
        var main = _particleSystem.main;
        main.loop = false;
        _particleSystem.Play();
    }
}