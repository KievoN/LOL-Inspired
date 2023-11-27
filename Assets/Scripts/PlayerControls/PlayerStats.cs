using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    [Header("Stats")]
    [Header("Damage")]
    public int AttackDamage = 50;
    public int AbilityPower = 0;

    [Header("Armors")]
    public int Armor = 50;
    public int MagicResist = 40;

    [Header("Attack modifiers")]
    public float AttackSpeed = 0.80f;
    public int AbilityHaste = 0;

    [Header("Others")]
    public int CritChance = 0;
    public int MoveSpeed = 350;
    public int AttackRange = 550;
}
