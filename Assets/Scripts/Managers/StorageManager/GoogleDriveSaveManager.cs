using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[CreateAssetMenu(fileName = "GoogleDriveSaveManager", menuName = "Manager/GoogleDriveSaveManager")]
public class GoogleDriveSaveManager : SaveManager
{
    [SerializeField] private string m_appKey;
    [SerializeField] private string m_callbackUri = "http://localhost";

    [ContextMenu("Authenticate")]
    private void DummyAuthenticate()
    {
        Authenticate(null);
    }

    private void Authenticate(Action<bool> onOauthComplete)
    {
        Managers.instance.GetManager<CoroutineManager>().StartCoroutine(StartOauth(onOauthComplete));
    }

    private IEnumerator StartOauth(Action<bool> onOauthComplete)
    {
        using (UnityWebRequest request = UnityWebRequest.Post("https://api.dropboxapi.com/1/oauth/request_token", string.Empty))
        {
            /*bool isCode = string.IsNullOrEmpty(m_callbackUri);
            request.SetRequestHeader("response_type", isCode? "code" : "token");
            request.SetRequestHeader("client_id", m_appKey);

            if(!isCode)
            {
                request.SetRequestHeader("redirect_uri", m_callbackUri);   
            }

            Debug.Log(request.uri);*/

            yield return request.Send();

            Debug.Log(request.responseCode);
        }
    }

    public override void LoadJson<T>(string name, Action<T> onComplete)
    {
        throw new NotImplementedException();
    }

    public override void SaveJson<T>(string name, T inputObject, Action<bool> onComplete)
    {
        throw new NotImplementedException();
    }
}
