using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Range = 0,
    Movement,
    WeaponSkill,
    BallisticSkill,
    Strength,
    Thoughness,
    Wounds,
    Attacks,
    Leadership,
    Save,
    InvulnerableSave,
    FeelNoPain
}

[Serializable]
public class GameStat
{
    public static Dictionary<StatType, Pair<string, string>> labels = new Dictionary<StatType, Pair<string, string>>
    {
        {StatType.Movement, new Pair<string, string>{first = "Movement", second = "M"}},
        {StatType.WeaponSkill, new Pair<string, string>{first = "Weapon Skill", second = "WS"}},
        {StatType.BallisticSkill, new Pair<string, string>{first = "Ballistic Skill", second = "BS"}},
        {StatType.Strength, new Pair<string, string>{first = "Strength", second = "S"}},
        {StatType.Thoughness, new Pair<string, string>{first = "Thoughness", second = "T"}},
        {StatType.Wounds, new Pair<string, string>{first = "Wounds", second = "W"}},
        {StatType.Attacks, new Pair<string, string>{first = "Attacks", second = "A"}},
        {StatType.Leadership, new Pair<string, string>{first = "Leadership", second = "Ld"}},
        {StatType.Save, new Pair<string, string>{first = "Save", second = "Sv"}},
        {StatType.Range, new Pair<string, string>{first = "Range", second = "R"}},
        {StatType.InvulnerableSave, new Pair<string, string>{first = "Invulnerable Save", second = "InvSv"}},
        {StatType.FeelNoPain, new Pair<string, string>{first = "Feel No Pain", second = "FnP"}}
    };

    [SerializeField] private StatType m_statType = StatType.Movement;
    [SerializeField] private int m_StatValue = 0;

    public StatType statType { get { return m_statType; }}
    public int statOffset { get; set; }
    public int statValue { get { return m_StatValue + statOffset; }}
    public string label { get { return labels[m_statType].first; }}
    public string shortLabel { get { return labels[m_statType].second; } }

    public GameStat(StatType statType)
    {
        m_statType = statType;
        statOffset = 0;
    }
}
