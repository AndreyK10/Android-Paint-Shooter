using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public enum TargetType { ScoreTarget, DamageTarget }

    public TargetType type { get; protected set; }
    public int value { get; protected set; }
}
