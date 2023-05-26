using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class BuildResources
{
    [MenuItem("BuildAB/Android")]
    public static void BuildAddressableAndroid()
    {
        UnityEngine.Debug.Assert(EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android);
        UpdateBuildAddressableToTarget();
    }
    [MenuItem("BuildAB/IOS")]
    public static void BuildAddressableiOS()
    {
        UnityEngine.Debug.Assert(EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS);
        UpdateBuildAddressableToTarget();
    }
    [MenuItem("BuildAB/WebGL")]
    public static void BuildAddressableWebGL()
    {
        UnityEngine.Debug.Assert(EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL);
        UpdateBuildAddressableToTarget();
    }
    [MenuItem("BuildAB/Win64")]
    public static void BuildAddressableWin64()
    {
        UnityEngine.Debug.Assert(EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64);
        UpdateBuildAddressableToTarget();
    }

    [MenuItem("BuildAB/OSX")]
    public static void BuildAddressableOSXUniversal()
    {
        UnityEngine.Debug.Assert(EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneOSX);
        UpdateBuildAddressableToTarget();
    }
    public static void UpdateBuildAddressableToTarget()
    {
        // AddressableAssetSettings setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>("Assets/AddressableAssetsData/AddressableAssetSettings.asset");
        // BuildAddressableToTarget(setting);
        AssetBundleTools.Editor.BuildAssetBundle.BuildAuto();
        AssetDatabase.Refresh();
        UnityEngine.Debug.Log("Addressable Created Finished!");
    }
    // private static void ClearCached(AddressableAssetSettings setting)
    // {
    //     var plantform = EditorUserBuildSettings.activeBuildTarget;
    //     var remDirs = new string[] { "./Addressables/" + plantform.ToString(), "./Library/com.unity.addressables" };
    //     foreach (var dir in remDirs)
    //     {
    //         if (Directory.Exists(dir))
    //         {
    //             UnityEngine.Debug.Log("Remove cached dir:" + dir);
    //             var dirs = System.IO.Directory.GetDirectories(dir);
    //             foreach (var subDir in dirs)
    //             {
    //                 System.IO.Directory.Delete(subDir, true);
    //             }
    //             System.IO.Directory.Delete(dir, true);
    //         }
    //     }
    // }
    // private static void BuildAddressableToTarget(AddressableAssetSettings setting)
    // {
    //     if (setting != null)
    //     {
    //         ClearCached(setting);
    //         var changed = false;
    //         if (setting.ActivePlayerDataBuilderIndex != 3)
    //         {
    //             setting.ActivePlayerDataBuilderIndex = 3;
    //             changed = true;
    //             UnityEngine.Debug.LogWarning("ActivePlayerDataBuilderIndex not right fixed!");
    //         }
    //         if (setting.ActivePlayModeDataBuilderIndex != 2)
    //         {
    //             changed = true;
    //             setting.ActivePlayModeDataBuilderIndex = 2;
    //             UnityEngine.Debug.LogWarning("ActivePlayModeDataBuilderIndex not right fixed!");
    //         }
    //         if (changed)
    //         {
    //             EditorUtility.SetDirty(setting);
    //         }
    //         AddressableAssetSettings.BuildPlayerContent();
    //     }
    // }
}