using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTeamMonoBehaviour : MonoBehaviour 
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
