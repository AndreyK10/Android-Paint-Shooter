using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleLauncher;
    [SerializeField]
    private ParticleSystem _splatterParticles;

    [SerializeField]
    private Camera _camera;
    private float _time;
    [SerializeField]
    private float _timeLimit;

    private List<ParticleCollisionEvent> _collisionEvents;

    public Gradient ParticleColorGradient;

    public DecalPool DecalPool;

    [SerializeField]
    private GameState GameState;
    private bool _canShoot;
    private void OnEnable()
    {
        GameState.OnStateChanged += OnStateChanged;
    }
    private void OnStateChanged(GameState.GameMode gameMode)
    {
        if (gameMode == GameState.GameMode.Menu || gameMode == GameState.GameMode.Lose)
        {
            _canShoot = false;
        }
        else
        {
            _canShoot = true;
        }
    }

    private void Start()
    {
        _collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void Update()
    {
        if (_canShoot)
        {
            if (Input.GetMouseButton(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(ray.origin, hit.point);
                    transform.LookAt(hit.point);
                    _time += Time.deltaTime;
                    if (_time >= _timeLimit)
                    {
                        _time = 0f;
                        EmitMainParticles();
                    }
                }
            }
        }
    }
    private void OnParticleCollision(GameObject other)
    {

        if (other.TryGetComponent(out Environment environment))
        {
            ParticlePhysicsExtensions.GetCollisionEvents(_particleLauncher, other, _collisionEvents);

            for (int i = 0; i < _collisionEvents.Count; i++)
            {
                DecalPool.ParticleHit(_collisionEvents[i], ParticleColorGradient);
                EmitAtLocation(_collisionEvents[i]);
            }
        }
    }

    private void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        _splatterParticles.transform.position = particleCollisionEvent.intersection;

        if (particleCollisionEvent.normal != Vector3.zero)
        {
            _splatterParticles.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        }        
        SetParticleColor(_splatterParticles);
        _splatterParticles.Emit(1);
    }

    private void EmitMainParticles()
    {
        SetParticleColor(_particleLauncher);
        _particleLauncher.Emit(1);
    }

    private void SetParticleColor(ParticleSystem particleSystem)
    {
        ParticleSystem.MainModule psMain = particleSystem.main;
        psMain.startColor = ParticleColorGradient.Evaluate(Random.Range(0f, 1f));
    }

    private void OnDisable()
    {
        GameState.OnStateChanged -= OnStateChanged;
    }
}
