using UnityEngine;

public class DamageTarget : Target
{
    public int Value;
    private void OnEnable()
    {
        type = TargetType.DamageTarget;
        value = Value;
    }
}
