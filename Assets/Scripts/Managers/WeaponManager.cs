using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponManager", menuName = "Manager/WeaponManager")]
public class WeaponManager : ScriptableManager
{
    [SerializeField] private List<Weapon> m_Weapons = new List<Weapon>();

    public Weapon GetWeapon(string weaponName)
    {
        return m_Weapons.Find(weapon => weapon != null && weapon.name == weaponName);
    }
}