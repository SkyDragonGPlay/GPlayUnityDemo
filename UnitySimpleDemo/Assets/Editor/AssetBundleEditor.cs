using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class AssetBundleEditor
{
//    [MenuItem("AssetBundle/Load Test scene")]
//    static void LoadTestScene()
//    {
//        if (EditorApplication.isPlaying && Selection.activeObject != null)
//        {
//            string filePath = AssetDatabase.GetAssetPath(Selection.activeObject);
//            if (File.Exists(filePath))
//            {
//                AssetBundle assetbundle = AssetBundle.LoadFromFile(filePath);
//                if (assetbundle != null)
//                {
//                    UnityEngine.SceneManagement.SceneManager.LoadScene("Test");
//                    assetbundle.Unload(false);
//                }
//            }
//        }
//    }

//    [MenuItem("AssetBundle/Build selected object")]
//    static void BuildAssetBundle()
//    {
//        //string assetPath = AssetDatabase.GUIDToAssetPath("95a6f842e1d2a6e4eb48d3eacf1c2911");

//        //if(!string.IsNullOrEmpty(assetPath))
//        //{
//        //    var assetObj = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Object));
//        //    if(assetObj != null)
//        //        EditorGUIUtility.PingObject(assetObj);
//        //}
//        if (Selection.objects.Length <= 0)
//            return;

//        List<string> lstAssetObj = new List<string>();
//        for (int i = 0; i < Selection.objects.Length; ++i)
//        {
//            string assetPath = AssetDatabase.GetAssetPath(Selection.objects[i]);
//            if (File.Exists(assetPath))
//            {
//                lstAssetObj.Add(assetPath);
//                Debug.Log(assetPath);
//            }
//        }

//        if (lstAssetObj.Count > 0)
//        {
//            AssetBundleBuild abb = new AssetBundleBuild();
//            abb.assetBundleName = Path.GetFileNameWithoutExtension(lstAssetObj[0]) + ".ab";
//            abb.assetNames = lstAssetObj.ToArray();

//            string outputPath = Path.Combine(Application.streamingAssetsPath, "AssetBundles");
//            if (!Directory.Exists(outputPath))
//                Directory.CreateDirectory(outputPath);

//#if UNITY_ANDROID
//            BuildPipeline.BuildAssetBundles(outputPath, new AssetBundleBuild[] { abb }, BuildAssetBundleOptions.None, BuildTarget.Android);
//#endif
//        }
//    }

//    [MenuItem("AssetBundle/List assets")]
//    static void ListAssets()
//    {
//        if (Selection.activeObject == null || !EditorApplication.isPlaying)
//            return;

//        string filePath = AssetDatabase.GetAssetPath(Selection.activeObject);
//        if (File.Exists(filePath))
//        {
//            AssetBundle assetbundle = AssetBundle.LoadFromFile(filePath);
//            if (assetbundle != null)
//            {
//                foreach (string asset in assetbundle.GetAllAssetNames())
//                {
//                    Debug.Log(asset);
//                    GameObject go = assetbundle.LoadAsset<GameObject>(Path.GetFileName(asset));
//                    Object.Instantiate(go);
//                }
//                //    Object.Instantiate(assetbundle.mainAsset);
//                assetbundle.Unload(false);
//            }
//        }
//    }
}
