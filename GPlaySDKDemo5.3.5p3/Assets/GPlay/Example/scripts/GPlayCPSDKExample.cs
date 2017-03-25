using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GPlay;
using System.Reflection;
using System;

public class GPlayCPSDKExample : MonoBehaviour
{
    public enum EState
    {
        Normal,
        Login
    }
    private EState State { get; set; }

    public string loadLevelName = "GPlayDemo_Level0";
    [SerializeField] private string appKey, appSecret, privateKey;

    private bool isPreloading;

    private GUIStyle m_GUIStyle;
    private string m_labelText;
    private List<string> m_lstButtonNames;
    private int m_btnWidth = 160, m_btnHeight = 60;

    private Texture2D texResources, texWWW;
    private AudioSource audioSource;

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //texResources = Resources.Load<Texture2D>("ResourcesTexture");
        //StartCoroutine(LoadStreamingTexture("WWWTexture.png"));
        m_lstButtonNames = new List<string>() { "IsInGplayEnv", "InitSDK", "GetChannelID", "GetNetworkType", "IsLogined",
                                "GetUserID", "Login", "QuitGame", "Share", "Pay", "CreateShortcut", "IsFunctionSupported",
                                "CallSyncFunc", "CallAsyncFunc", "PreloadResourceBundle", "PreloadResourceBundles", "StartGame",
                                "Test"};

        m_btnWidth = Screen.width / 4;
        m_btnHeight = Screen.height / 2 / (m_lstButtonNames.Count / 4 + 1);
    }

    private int audioCount = 0;
    private bool isTesting = false;
    private int testFrameIntervalCount = 0;
    void Update()
    {
        if (isTesting)
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
            audioSource.clip = Resources.Load<AudioClip>(audioCount.ToString());
            audioSource.Play();
            m_labelText = "Audio Count: " + audioCount;
            audioCount = (audioCount + 1) % 3608;
        }
    }

    void OnGUI()
    {
        m_GUIStyle = new GUIStyle(GUI.skin.button);
        m_GUIStyle.fontSize = 20;
        switch (State)
        {
            case EState.Normal: DrawNormalLayout(); break;
        }
    }

    private void DrawNormalLayout()
    {
        const int colCount = 4;
        for(int i=0; i< m_lstButtonNames.Count; i+=colCount)
        {
            GUILayout.BeginHorizontal();
            for (int j=i; j<i+colCount && j<m_lstButtonNames.Count; ++j)
            {
                if (GUILayout.Button(m_lstButtonNames[j], m_GUIStyle, GUILayout.Width(m_btnWidth), GUILayout.Height(m_btnHeight)))
                {
                    m_labelText = m_lstButtonNames[j];

//#if !UNITY_EDITOR
                    string methodName = m_lstButtonNames[j] + "BtnClick";
                    Type type = this.GetType();
                    MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
                    if (methodInfo != null)
                        methodInfo.Invoke(this, null);
//#endif
                }
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.Label(m_labelText);

        GUILayout.BeginHorizontal();
        if (texResources != null)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("Resources.Load Texture: ");
            GUILayout.Label(new GUIContent(texResources));
            GUILayout.EndVertical();
        }
        if (texWWW != null)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("WWW Load Texture: ");
            GUILayout.Label(new GUIContent(texWWW));
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }

#region GPlayCPSDK callbacks
    private void LoginCallback(EActionResultCode resultCode, string jsonResult)
    {
        m_labelText = string.Format("LoginCallback:\nresult code : {0}\njson Result: {1}", resultCode, jsonResult);
    }

    private void ShareCallback(EActionResultCode resultCode, string jsonResult)
    {
        m_labelText = string.Format("ShareCallback\nresult code : {0}\njson Result: {1}", resultCode, jsonResult);
    }

    private void PayCallback(EActionResultCode resultCode, string jsonResult)
    {
        m_labelText = string.Format("PayCallback\nresult code : {0}\njson Result: {1}", resultCode, jsonResult);
    }

    private void CreateShortcutCallback(EActionResultCode resultCode, string jsonResult)
    {
        m_labelText = string.Format("CreateShotCutCallback\nresult code : {0}\njson Result: {1}", resultCode, jsonResult);
    }

    private void CallAsyncCallback(EActionResultCode resultCode, string jsonResult)
    {
        m_labelText = string.Format("CallAsyncCallback\nresult code : {0}\njson Result: {1}", resultCode, jsonResult);
    }

    private void PreloadResponse(PreloadResponseInfo info)
    {
        m_labelText = string.Format("Preload Response :\ndownloadSpeed: {0}    errorCode: {1}   groupName: {2}    percent: {3}   resultCode: {4}",
                                        info.downloadSpeed, info.errorCode, info.groupName, info.percent, info.resultCode);

        switch(info.resultCode)
        {
            case EActionResultCode.PRELOAD_RESULT_SUCCESS:
                break;

            case EActionResultCode.PRELOAD_RESULT_PROGRESS:
                string infoStr = string.Format("Preload Response :\ndownloadSpeed: {0}    errorCode: {1}   groupName: {2}    percent: {3}   resultCode: {4}",
                                        info.downloadSpeed, info.errorCode, info.groupName, info.percent, info.resultCode);

                Debug.Log(infoStr);
                break;
        }
        if(info.resultCode == EActionResultCode.PRELOAD_RESULT_SUCCESS)
        {
            if (info.groupName == "ResourcesTexture")
                texResources = Resources.Load<Texture2D>("GPlay");
            else if (info.groupName == "WWWTexture")
            {
                StartCoroutine(LoadStreamingTexture("WWWTexture.png"));
            }
        }
    }

    private IEnumerator LoadStreamingTexture(string textureName)
    {
#if UNITY_EDITOR
        string path = "file:///" + Application.streamingAssetsPath + "/" + textureName;
#elif UNITY_ANDROID
        string path = Application.streamingAssetsPath + "/" + textureName;
#endif
        using (WWW www = new WWW(path))
        {
            while (!www.isDone)
                yield return null;

            if (!string.IsNullOrEmpty(www.error))
                Debug.Log(www.error);
            else
            {
                Debug.Log("www.isDone");
                texWWW = www.texture;
            }
        }
    }
#endregion

#region GUI Buttons' callback
    private void StartGameBtnClick()
    {
        Debug.Log("Start Game btn clik");
        UnityEngine.SceneManagement.SceneManager.LoadScene(loadLevelName);
    }

    private void TestBtnClick()
    {
        isTesting = !isTesting;
        m_labelText = "audioCount : " + audioCount;
    }

    private void IsInGplayEnvBtnClick()
    {
        m_labelText = "IsInGplayEnv: " + GPlayCPSDK.IsInGplayEnv();
    }

    private void InitSDKBtnClick()
    {
        m_labelText = "IsInGplayEnvBtnClick";
        GPlayCPSDK.InitSDK(appKey, appSecret, privateKey);
    }

    private void GetChannelIDBtnClick()
    {
        m_labelText = "ChannelID : " + GPlayCPSDK.GetChannelID();
    }

    private void GetNetworkTypeBtnClick()
    {
        m_labelText = "NetworkType : " + GPlayCPSDK.GetNetworkType();
    }

    private void IsLoginedBtnClick()
    {
        m_labelText = "IsLogined : " + GPlayCPSDK.IsLogined();
    }

    private void GetUserIDBtnClick()
    {
        m_labelText = "UserID : " + GPlayCPSDK.GetUserID();
    }

    private void LoginBtnClick()
    {
        m_labelText = "LoginBtnClick";
        GPlayCPSDK.Login(LoginCallback);
    }

    private void QuitGameBtnClick()
    {
        m_labelText = "QuitGameBtnClick";
        GPlayCPSDK.QuitGame();
    }

    private void ShareBtnClick()
    {
        m_labelText = "ShareBtnClick";
        GPlayShareParams shareInfo = new GPlayShareParams();
        GPlayCPSDK.Share(shareInfo, ShareCallback);
    }

    private void PayBtnClick()
    {
        m_labelText = "PayBtnClick";
        GPlayPayParams payInfo = new GPlayPayParams();
        GPlayCPSDK.Pay(payInfo, PayCallback);
    }

    private void CreateShortcutBtnClick()
    {
        m_labelText = "CreateShortcutBtnClick";
        GPlayCPSDK.CreateShortcut(CreateShortcutCallback);
    }

    private void IsFunctionSupportedBtnClick()
    {
        m_labelText = "Is Function Supported : " + GPlayCPSDK.IsFunctionSupported("");
    }

    private void CallSyncFuncBtnClick()
    {
        m_labelText = "CallSyncFuncBtnClick";
        GPlayCPSDK.CallSyncFunc("", "");
    }

    private void CallAsyncFuncBtnClick()
    {
        m_labelText = "CallAsyncFuncBtnClick";
        GPlayCPSDK.CallAsyncFunc("", "", CallAsyncCallback);
    }

    public void OnPreloadGroupSuccess()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("sceneName");
    }

    private void PreloadResourceBundleBtnClick()
    {
        if (isPreloading)
            return;

        m_labelText = "PreloadResourceBundleBtnClick";
        GPlayCPSDK.PreloadGroup("group1", OnPreloadGroupSuccess);
        //GPlayCPSDK.PreloadGroup("ResourcesTexture", PreloadResponse);
    }

    private void PreloadResourceBundlesBtnClick()
    {
        if (isPreloading)
            return;

        m_labelText = "PreloadResourceBundlesBtnClick";
        GPlayCPSDK.PreloadGroups(new string[] { "WWWTexture" }, PreloadResponse);
    }
#endregion
}
