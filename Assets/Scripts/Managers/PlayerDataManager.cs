using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataManager", menuName = "Manager/PlayerDataManager")]
public class PlayerDataManager : ScriptableManager
{
    private const string FILE_NAME = "killTeamPlayerProfile.json";

    [SerializeField] private PlayerProfile m_PlayerProfile = null;

    public override void Initialize()
    {
        base.Initialize();
        LoadProfile();
    }

    [ContextMenu("Load Profile")]
    public void LoadProfile()
    {
        Managers.instance.GetManager<SaveManager>().LoadJson<PlayerProfile>(FILE_NAME, (result) =>
        {
            m_PlayerProfile = result;
        });
    }

    [ContextMenu("Save Profile")]
    public void SaveProfile()
    {
        Managers.instance.GetManager<SaveManager>().SaveJson<PlayerProfile>(FILE_NAME, m_PlayerProfile, (success) => {
            if(!success)
            {
                Debug.LogError("Failed Saving the profile");
            }
        });
    }
}
