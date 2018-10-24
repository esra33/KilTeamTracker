using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTeamScriptableObject : ScriptableObject 
{
    protected void Awake()
    {
        Managers.instance.Inject(this);
        OnAwake();
    }

    protected virtual void OnAwake()
    {

    }
}
