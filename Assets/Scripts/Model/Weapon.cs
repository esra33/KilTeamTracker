using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Meele,
    Assault,
    Heavy,
    RapidFire,
    Grenade,
    Pistol
}

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Model/Weapon")]
public class Weapon : ScriptableObject 
{
    public WeaponType weaponType;
    public int range;
    public int strength;
    public int armorPenetration;
    public bool RandomAttacks;
    public int attacks;
    public bool randomDamage = false;
    public int damage = 1;
    public string specialAbilities;
}
