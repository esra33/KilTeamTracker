using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterData : ICharacterStats
{
    [SerializeField]
    [LinkableAsset(targetType = typeof(SpecificArchetype))] 
    private string m_ArchetypeUid;

    [SerializeField] 
    [LinkableAsset(targetType = typeof(Weapon))]
    private List<string> m_WeaponsUids;

    public bool isDead = false;
    public int experience = 0;

    private SpecificArchetype _archetype = null;
    public SpecificArchetype archetype
    {
        get
        {
            if (_archetype == null && !string.IsNullOrEmpty(m_ArchetypeUid))
            {
                _archetype = Managers.instance.GetManager<ArchetypeManager>().GetArchetype<SpecificArchetype>(m_ArchetypeUid);
            }

            return _archetype;
        }
    }

    private List<Weapon> _weapons = null;
    public List<Weapon> weapons
    {
        get
        {
            if(_weapons == null)
            {
                WeaponManager weaponManager = Managers.instance.GetManager<WeaponManager>();
                _weapons = m_WeaponsUids.ConvertAll<Weapon>(weaponuid => weaponManager.GetWeapon(weaponuid));
            }

            return _weapons;
        }
    }

    public ArchetypeType GetArchetypeType()
    {
        return archetype.GetArchetypeType();
    }

    public int GetLimit()
    {
        return archetype.GetLimit();
    }

    public GameStat GetMovement()
    {
        return archetype.GetMovement();
    }

    public GameStat GetWeaponSkill()
    {
        return archetype.GetWeaponSkill();
    }

    public GameStat GetBallisticSkill()
    {
        return archetype.GetBallisticSkill();
    }

    public GameStat GetStrength()
    {
        return archetype.GetStrength();
    }

    public GameStat GetThoughness()
    {
        return archetype.GetThoughness();
    }

    public GameStat GetWounds()
    {
        return archetype.GetWounds();
    }

    public GameStat GetAttacks()
    {
        return archetype.GetAttacks();
    }

    public GameStat GetLeadership()
    {
        return archetype.GetLeadership();
    }

    public GameStat GetArmourSave()
    {
        return archetype.GetArmourSave();
    }

    public GameStat GetInvulnerableSave()
    {
        return archetype.GetInvulnerableSave();
    }

    public GameStat GetFeelNoPain()
    {
        return archetype.GetFeelNoPain();
    }
}