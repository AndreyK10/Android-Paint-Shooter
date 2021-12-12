using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    public delegate void TargetHit(Target.TargetType targetType ,int value);
    public event TargetHit OnTargetHit;



    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out Target target))
        {
            OnTargetHit?.Invoke(target.type, target.value);
            target.gameObject.SetActive(false);
        }
    }

}
