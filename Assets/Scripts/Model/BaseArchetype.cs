using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBaseArchetype", menuName = "Model/BaseArchetype")]
public class BaseArchetype : AbstractArchetype 
{
    public List<GameStat> archetypeStats = new List<GameStat>();
    private Dictionary<StatType, GameStat> activeStats = new Dictionary<StatType, GameStat>();

    private void OnEnable()
    {
        SetupDictionary();
    }

    private void SetupDictionary()
    {
        activeStats = new Dictionary<StatType, GameStat>();
        foreach(GameStat stat in archetypeStats)
        {
            activeStats[stat.statType] = stat;
        }
    }

    // Stats
    public override ArchetypeType GetArchetypeType()
    {
        return ArchetypeType.None;
    }

    public override GameStat GetArmourSave()
    {
        return activeStats[StatType.Save];
    }

    public override GameStat GetAttacks()
    {
        return activeStats[StatType.Attacks];
    }

    public override GameStat GetBallisticSkill()
    {
        return activeStats[StatType.BallisticSkill];
    }

    public override GameStat GetFeelNoPain()
    {
        return activeStats[StatType.FeelNoPain];
    }

    public override GameStat GetInvulnerableSave()
    {
        return activeStats[StatType.InvulnerableSave];
    }

    public override GameStat GetLeadership()
    {
        return activeStats[StatType.Leadership];
    }

    public override int GetLimit()
    {
        return 0;
    }

    public override GameStat GetMovement()
    {
        return activeStats[StatType.Movement];
    }

    public override string GetName()
    {
        return name;
    }

    public override GameStat GetStrength()
    {
        return activeStats[StatType.Strength];
    }

    public override GameStat GetThoughness()
    {
        return activeStats[StatType.Thoughness];
    }

    public override GameStat GetWeaponSkill()
    {
        return activeStats[StatType.WeaponSkill];
    }

    public override GameStat GetWounds()
    {
        return activeStats[StatType.Wounds];
    }

#if UNITY_EDITOR
    private bool isEditing = false;
    public void OnValidate()
    {
        if (isEditing)
        {
            return;
        }

        isEditing = true;
        SetupDictionary();
        archetypeStats.Clear();
        foreach(KeyValuePair<StatType, GameStat> stat in activeStats)
        {
            archetypeStats.Add(stat.Value);
        }

        archetypeStats.Sort((itemA, itemB) =>
        {
            return ((int)itemA.statType).CompareTo((int)itemB.statType);
        });

        isEditing = false;
    }

    [ContextMenu("Stand Up Archetype")]
    private void AutoFill()
    {
        isEditing = true;
        archetypeStats = CreateGameStatList();
        isEditing = false;
    }

    private List<GameStat> CreateGameStatList()
    {
        List<GameStat> stats = new List<GameStat>();
        stats.Add(new GameStat(StatType.Movement));
        stats.Add(new GameStat(StatType.WeaponSkill));
        stats.Add(new GameStat(StatType.BallisticSkill));
        stats.Add(new GameStat(StatType.Strength));
        stats.Add(new GameStat(StatType.Thoughness));
        stats.Add(new GameStat(StatType.Wounds));
        stats.Add(new GameStat(StatType.Attacks));
        stats.Add(new GameStat(StatType.Leadership));
        stats.Add(new GameStat(StatType.Save));
        stats.Add(new GameStat(StatType.InvulnerableSave));
        stats.Add(new GameStat(StatType.FeelNoPain));

        return stats;
    }
#endif
}
