using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string _name = "[Default Ability]";

    [Header("-= Stats =-")]
    [Min(0.1f)] public float _cooldown = 1f;

    public enum AbilityKey
    {
        Q,
        W,
        E,
        R
    }
    public AbilityKey _key;

    public virtual void SpawnAbility(GameObject Instigator, Transform spawnPoint, Ability ability) { }
}
