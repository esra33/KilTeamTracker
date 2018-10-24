using UnityEngine;

public interface IManager
{
    void Initialize();
}

public class ScriptableManager : KillTeamScriptableObject, IManager
{
    public virtual void Initialize()
    {
        Awake();
    }
}
