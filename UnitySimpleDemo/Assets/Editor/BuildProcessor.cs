using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class BuildProcessor
{
    [MenuItem("BuildProcessor/Build Android")]
    public static void BuildAndroid()
    {
        string buildFileName = "GPlaySmallPackageTest.apk";
        string[] commandLineArgs = System.Environment.GetCommandLineArgs();
        if (commandLineArgs.Length != 0)
            buildFileName = commandLineArgs[commandLineArgs.Length-1];

        if (!buildFileName.EndsWith(".apk"))
            buildFileName += ".apk";

        string[] buildScenesPaths = new string[] { "Assets/Scene/test.unity" };

        PlayerSettings.bundleIdentifier = "com.gabo.test";
        //PlayerSettings.SetPropertyInt("DeviceFilter", 1, BuildTargetGroup.Android);
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
        PlayerSettings.allowedAutorotateToPortrait = PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
        PlayerSettings.allowedAutorotateToLandscapeLeft = PlayerSettings.allowedAutorotateToLandscapeRight = true;

        string targetFilePath = Path.GetDirectoryName(Application.dataPath) + "/" + buildFileName;
        BuildPipeline.BuildPlayer(buildScenesPaths, targetFilePath, BuildTarget.Android, BuildOptions.None);
    }
}
