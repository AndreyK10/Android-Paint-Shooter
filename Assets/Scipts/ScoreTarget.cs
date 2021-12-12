using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTarget : Target
{
    public int Value;
    private void OnEnable()
    {
        type = TargetType.ScoreTarget;
        value = Value;
    }
}
