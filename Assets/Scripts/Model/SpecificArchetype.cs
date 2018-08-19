using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpecificArchetype", menuName = "Model/SpecificArchetype")]
public class SpecificArchetype : AbstractArchetype 
{
    [SerializeField] private BaseArchetype m_BaseArchetype;
    [SerializeField] private ArchetypeType m_ArchetypeType = ArchetypeType.Regular;
    [SerializeField] private int m_Limit = 20;

    public override ArchetypeType GetArchetypeType()
    {
        return m_ArchetypeType;
    }

    public override GameStat GetArmourSave()
    {
        return m_BaseArchetype.GetArmourSave();
    }

    public override GameStat GetAttacks()
    {
        return m_BaseArchetype.GetAttacks();
    }

    public override GameStat GetBallisticSkill()
    {
        return m_BaseArchetype.GetBallisticSkill();
    }

    public override GameStat GetFeelNoPain()
    {
        return m_BaseArchetype.GetFeelNoPain();
    }

    public override GameStat GetInvulnerableSave()
    {
        return m_BaseArchetype.GetInvulnerableSave();
    }

    public override GameStat GetLeadership()
    {
        return m_BaseArchetype.GetLeadership();
    }

    public override int GetLimit()
    {
        return m_Limit;
    }

    public override GameStat GetMovement()
    {
        return m_BaseArchetype.GetMovement();
    }

    public override string GetName()
    {
        return name;
    }

    public override GameStat GetStrength()
    {
        return m_BaseArchetype.GetStrength();
    }

    public override GameStat GetThoughness()
    {
        return m_BaseArchetype.GetThoughness();
    }

    public override GameStat GetWeaponSkill()
    {
        return m_BaseArchetype.GetWeaponSkill();
    }

    public override GameStat GetWounds()
    {
        return m_BaseArchetype.GetWounds();
    }
}
