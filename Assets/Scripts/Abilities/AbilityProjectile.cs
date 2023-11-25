using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Projectile")]
public class AbilityProjectile : Ability
{
    [Min(0)] public int[] _damage = { 30, 50, 70 };
}
