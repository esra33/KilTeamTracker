using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataManager", menuName = "Manager/PlayerDataManager")]
public class PlayerDataManager : ScriptableManager
{
    [Injectable] private SaveManager m_saveManager;
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
        m_saveManager.LoadJson<PlayerProfile>(FILE_NAME, (result) =>
        {
            m_PlayerProfile = result;
        });
    }

    [ContextMenu("Save Profile")]
    public void SaveProfile()
    {
        m_saveManager.SaveJson<PlayerProfile>(FILE_NAME, m_PlayerProfile, (success) => {
            if(!success)
            {
                Debug.LogError("Failed Saving the profile");
            }
        });
    }
}
