using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

public class Test : MonoBehaviour
{
    private Texture wwwTexture, resTexture;
    
    void Awake()
    {
        Debug.Log(Application.persistentDataPath);
    }
    
    void OnGUI()
    {
        if(GUI.Button(new Rect(0, 0, Screen.width/ 3, Screen.height/5), "WWW"))
        {
            OnWWWLoadBtnClick();
        }

        if (GUI.Button(new Rect(Screen.width / 3, 0, Screen.width / 3, Screen.height / 5), "DownloadFile"))
        {
            // 下载文件，iOS 下没有工具操作文件系统，所以就用下载的
            //StartCoroutine(DownloadResourceFile());
        }

        if (GUI.Button(new Rect(Screen.width / 3 * 2, 0, Screen.width / 3, Screen.height / 5.0f), "Resource"))
        {
            OnResourcesLoadBtnClick();
        }

        if (wwwTexture != null)
        {
            GUI.DrawTexture(new Rect(0, Screen.height / 5.0f, Screen.width / 2.0f - 10, Screen.height * 4 / 5.0f), wwwTexture);
        }
        if (resTexture != null)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2.0f + 10, Screen.height / 5.0f, Screen.width / 2.0f - 10, Screen.height * 4 / 5.0f), resTexture);
        }
    }

    private IEnumerator DownloadResourceFile()
    {
        WWW www = new WWW("http://192.168.0.129:8080/resources.assets");

        yield return www;
        
        if(www.error != null)
        {
            Debug.LogError(www.error);
        }

        string filePath = Application.persistentDataPath + "/resources.assets";
        using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            fileStream.Write(www.bytes, 0, www.bytes.Length);
        }

        Debug.Log("done!!! " + filePath);

        //if (Application.loadedLevel == 0)
        //    Application.LoadLevel(1);
        //else
        //    Application.LoadLevel(0);
    }

    private void OnResourcesLoadBtnClick()
    {
        Debug.Log("OnResourcesLoadBtnClick");

        resTexture = Resources.Load<Texture>("texture");
    }

    private void OnWWWLoadBtnClick()
    {
        Debug.Log("OnWWWLoadBtnClick");
        StartCoroutine(LoadStreamingTexture());
    }

    private IEnumerator LoadStreamingTexture()
    {
#if UNITY_EDITOR
        string fileURL = "file:///" + Path.Combine(Application.streamingAssetsPath, "wwwtexture.jpg");
#else
        string fileURL = Path.Combine(Application.streamingAssetsPath, "wwwtexture.jpg");
#endif

        WWW www = new WWW(fileURL);

        while (!www.isDone)
            yield return null;

        if (www.error != null)
            Debug.LogError(www.error);

        wwwTexture = www.texture;
    }

    private IEnumerator LoadAssetBundleTexture()
    {
#if UNITY_EDITOR
        string abFile = "file:///" + Path.Combine(Application.streamingAssetsPath, "AssetBundles/texture.ab");
#else
        string abFile = Path.Combine(Application.streamingAssetsPath, "AssetBundles/texture.ab");
#endif

        WWW www = new WWW(abFile);
        yield return www;

        //if(www.error != null)
        //{
        //    Debug.LogError(www.error);
        //}
        //else if(www.assetBundle != null)
        //{
        //    Sprite sprite = www.assetBundle.LoadAsset<Sprite>("Unity脚本生命周期");
        //    if (sprite != null)
        //    {
        //        wwwImage.sprite = sprite;
        //    }
        //    else
        //    {
        //        Debug.LogError("sprite is null");
        //    }
        //    www.assetBundle.Unload(false);
        //}
        //else
        //{
        //    Debug.LogError("assetbundle is null");
        //}
        //www.Dispose();
    }
}
