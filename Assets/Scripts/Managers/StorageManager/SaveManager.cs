using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveManager : ScriptableManager 
{
    public abstract void SaveJson<T>(string name, T inputObject, Action<bool> onComplete);
    public abstract void LoadJson<T>(string name, Action<T> onComplete);
}
