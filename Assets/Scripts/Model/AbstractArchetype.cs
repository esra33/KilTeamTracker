using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArchetypeType
{
    None,
    Regular,
    Gunner,
    Fighter,
    Leader
}    

public interface ICharacterStats
{
    ArchetypeType GetArchetypeType();
    int GetLimit();
    GameStat GetMovement();
    GameStat GetWeaponSkill();
    GameStat GetBallisticSkill();
    GameStat GetStrength();
    GameStat GetThoughness();
    GameStat GetWounds();
    GameStat GetAttacks();
    GameStat GetLeadership();
    GameStat GetArmourSave();
    GameStat GetInvulnerableSave();
    GameStat GetFeelNoPain();
}

public abstract class AbstractArchetype : ScriptableObject, ICharacterStats
{
    public abstract string GetName();
    public abstract ArchetypeType GetArchetypeType();
    public abstract int GetLimit();

    // Stats
    public abstract GameStat GetMovement();
    public abstract GameStat GetWeaponSkill();
    public abstract GameStat GetBallisticSkill();
    public abstract GameStat GetStrength();
    public abstract GameStat GetThoughness();
    public abstract GameStat GetWounds();
    public abstract GameStat GetAttacks();
    public abstract GameStat GetLeadership();
    public abstract GameStat GetArmourSave();
    public abstract GameStat GetInvulnerableSave();
    public abstract GameStat GetFeelNoPain();
}
