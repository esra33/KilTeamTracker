using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArchetypeManager", menuName = "Manager/ArchetypeManager")]
public class ArchetypeManager : ScriptableManager 
{
    [SerializeField] private List<AbstractArchetype> m_Archetypes = new List<AbstractArchetype>();

    public T GetArchetype<T>(string archetypeName) where T : AbstractArchetype
    {
        List<AbstractArchetype> possibleArchetypes = m_Archetypes.FindAll(archetype => archetype != null && archetype.GetName() == archetypeName);
        foreach (AbstractArchetype archetype in possibleArchetypes)
        {
            if (archetype is T)
            {
                return (T)archetype;
            }
        }

        return null;
    }
}
