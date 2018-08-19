using UnityEngine;

public interface IManager
{
    void Initialize();
}

public class ScriptableManager : ScriptableObject, IManager
{
    public virtual void Initialize(){}
}
