using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DecalPool : MonoBehaviour
{
    public int MaxDecals = 100;
    public float DecalSizeMin, DecalSizeMax;

    private ParticleSystem _decalParticleSystem;
    private int _decalDataIndex;
    private DecalData[] _decalData;
    private ParticleSystem.Particle[] _particles; 

    private Vector3 _particleRotationEuler;

    [SerializeField]
    private GameState GameState;

    private void OnEnable()
    {
        GameState.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode != GameState.GameMode.Lose)
        {
            StartCoroutine("CleanParticles");
        }
    }

    private void Start()
    {

        _decalParticleSystem = GetComponent<ParticleSystem>();

        if (MaxDecals > _decalParticleSystem.main.maxParticles)
        {
            MaxDecals = _decalParticleSystem.main.maxParticles;
        }

        _particles = new ParticleSystem.Particle[MaxDecals];


        _decalData = new DecalData[MaxDecals];

        for (int i = 0; i < MaxDecals; i++)
        {
            _decalData[i] = new DecalData();
        }                
    }

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        SetParticleData(particleCollisionEvent, colorGradient);
        DisplayParticles();
    }

    private void SetParticleData(ParticleCollisionEvent particleCollisionEvent, Gradient colorGradient)
    {
        if (_decalDataIndex >= MaxDecals)
        {
            _decalDataIndex = 0;
        }

        _decalData[_decalDataIndex].Position = particleCollisionEvent.intersection;        
        if (particleCollisionEvent.normal != Vector3.zero)
        {
            _particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;            
        }
        _particleRotationEuler.z = Random.Range(0, 360);
        _decalData[_decalDataIndex].Rotation = _particleRotationEuler;
        _decalData[_decalDataIndex].Size = Random.Range(DecalSizeMin, DecalSizeMax);
        _decalData[_decalDataIndex].Color = colorGradient.Evaluate(Random.Range(0f, 1f));
        _decalDataIndex++;
    }

    private void DisplayParticles()
    {
        for (int i = 0; i < _decalData.Length; i++)
        {
            _particles[i].position = _decalData[i].Position;
            _particles[i].rotation3D = _decalData[i].Rotation;
            _particles[i].startSize = _decalData[i].Size;
            _particles[i].startColor = _decalData[i].Color;
        }
        _decalParticleSystem.SetParticles(_particles, _particles.Length);
    }

    private void OnDisable()
    {
        GameState.OnStateChanged -= OnStateChanged;
    }

    public void DeleteParticles()
    {
        StartCoroutine("CleanParticles");
    }

    private IEnumerator CleanParticles()
    {
        yield return new WaitForEndOfFrame();
        _decalParticleSystem.Clear();
        for (int i = 0; i < MaxDecals; i++)
        {
            _decalData[i] = new DecalData();
        }
    }


}
