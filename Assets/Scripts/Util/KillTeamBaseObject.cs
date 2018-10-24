using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTeamBaseObject 
{
    public KillTeamBaseObject()
    {
        Managers.instance.Inject(this);
    }
}
