using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;

[CreateAssetMenu(fileName = "GoogleDriveSaveManager", menuName = "Manager/GoogleDriveSaveManager")]
public class GoogleDriveSaveManager : SaveManager
{
    const string authorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    const string tokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
    const string userInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";

    [SerializeField] private string m_projectName;
    [SerializeField] private string m_projectId;
    [SerializeField] private string m_appId;
    [SerializeField] private string m_appSecret;
    [SerializeField] private long m_projectNumber;
    [SerializeField] private string m_callbackUri = "http://localhost";

    [ContextMenu("Authenticate")]
    private void DummyAuthenticate()
    {
        Authenticate(null);
    }

#region HELPER_FUNCTIONS
    public static int GetRandomUnusedPort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }

    /// <summary>
    /// Base64url no-padding encodes the given input buffer.
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static string base64urlencodeNoPadding(byte[] buffer)
    {
        string base64 = Convert.ToBase64String(buffer);

        // Converts base64 to base64url.
        base64 = base64.Replace("+", "-");
        base64 = base64.Replace("/", "_");
        // Strips padding.
        base64 = base64.Replace("=", "");

        return base64;
    }

    /// <summary>
    /// Returns URI-safe data with a given input length.
    /// </summary>
    /// <param name="length">Input length (nb. output will be longer)</param>
    /// <returns></returns>
    public static string randomDataBase64url(uint length)
    {
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        byte[] bytes = new byte[length];
        rng.GetBytes(bytes);
        return base64urlencodeNoPadding(bytes);
    }

    /// <summary>
    /// Returns the SHA256 hash of the input string.
    /// </summary>
    /// <param name="inputStirng"></param>
    /// <returns></returns>
    public static byte[] sha256(string inputStirng)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(inputStirng);
        SHA256Managed sha256 = new SHA256Managed();
        return sha256.ComputeHash(bytes);
    }
#endregion

    private void Authenticate(Action<bool> onOauthComplete)
    {
        //Managers.instance.GetManager<CoroutineManager>().StartCoroutine(StartOauth(onOauthComplete));
        StartOauth(onOauthComplete);
    }

    private void StartOauth(Action<bool> onOauthComplete)
    {
        // Generates state and PKCE values.
        string state = randomDataBase64url(32);
        string code_verifier = randomDataBase64url(32);
        string code_challenge = base64urlencodeNoPadding(sha256(code_verifier));
        const string code_challenge_method = "S256";

        // Creates a redirect URI using an available port on the loopback address.
        string redirectURI = string.Format("http://{0}:{1}/", IPAddress.Loopback, GetRandomUnusedPort());

        // Creates the OAuth 2.0 authorization request.
        string authorizationRequest = string.Format("{0}?response_type=code&scope=openid%20profile&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
            authorizationEndpoint,
            System.Uri.EscapeDataString(redirectURI),
            m_appId,
            state,
            code_challenge,
            code_challenge_method);

        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(redirectURI);
        listener.Start();

        // we actually need a listener to figure out when something comes down the pipes on the response code. Now I think this may work on iOS and Android as well but dunno 
        Application.OpenURL(authorizationRequest);

        // integrate as part of a coroutine maybe?
        HttpListenerContext context = listener.GetContext();
        HttpListenerRequest request = context.Request;

        // Sends an HTTP response to the browser.
        var response = context.Response;
        string responseString = string.Format("<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>");
        var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        var responseOutput = response.OutputStream;
       
        // Checks for errors.
        if (context.Request.QueryString.Get("error") != null)
        {
            output(String.Format("OAuth authorization error: {0}.", context.Request.QueryString.Get("error")));
            return;
        }
        if (context.Request.QueryString.Get("code") == null
            || context.Request.QueryString.Get("state") == null)
        {
            output("Malformed authorization response. " + context.Request.QueryString);
            return;
        }

        // extracts the code
        var code = context.Request.QueryString.Get("code");
        var incoming_state = context.Request.QueryString.Get("state");

        // Compares the receieved state to the expected value, to ensure that
        // this app made the request which resulted in authorization.
        if (incoming_state != state)
        {
            output(String.Format("Received request with invalid state ({0})", incoming_state));
            return;
        }
        output("Authorization code: " + code);

        Managers.instance.GetManager<CoroutineManager>().StartCoroutine(PerformCodeExchange(code, code_verifier, redirectURI));
    }

    private void output(string value)
    {
        Debug.Log(value);
    }

    private IEnumerator PerformCodeExchange(string code, string code_verifier, string redirectURI)
    {
        // builds the  request
        string tokenRequestURI = "https://www.googleapis.com/oauth2/v4/token";
        string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&client_secret={4}&scope=&grant_type=authorization_code",
            code,
            System.Uri.EscapeDataString(redirectURI),
            m_appId,
            code_verifier,
            m_appSecret
            );

        using (UnityWebRequest request = UnityWebRequest.Post(tokenRequestURI, tokenRequestBody))
        {
            yield return request.SendWebRequest();
            Debug.Log(request.responseCode);
            Debug.Log(request.downloadHandler.text);
        }
    }

    private IEnumerator PerformUserInfoCall(string access_tocken)
    {
        // builds the  request
        string userinfoRequestURI = "https://www.googleapis.com/oauth2/v3/userinfo";
        using (UnityWebRequest request = UnityWebRequest.Get(userinfoRequestURI))
        {
            request.SetRequestHeader("Authorization", "Bearer " + access_tocken);
            yield return request.SendWebRequest();
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
