using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatOnCollision : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleLauncher;
    public Gradient ParticleColorGradient;
    public DecalPool DropletDecalPool;

    private List<ParticleCollisionEvent> _collisionEvents;


    private void Start()
    {
        _collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = ParticlePhysicsExtensions.GetCollisionEvents(_particleLauncher, other, _collisionEvents);

        int i = 0;
        while (i < numCollisionEvents)
        {
            DropletDecalPool.ParticleHit(_collisionEvents[i], ParticleColorGradient);
            i++;
        }

    }
}
