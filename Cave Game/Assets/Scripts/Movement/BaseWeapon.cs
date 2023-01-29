using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    melee,ranged,other
}
public class BaseWeapon
{
    public string Name;
    public WeaponTypes type;
    public string Damage;
}
