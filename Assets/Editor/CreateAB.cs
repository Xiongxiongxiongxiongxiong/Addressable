using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateAB : Editor
{
    [MenuItem("Tool / Create AB")]
   static void Creator()
    {
       // 创建了一个名为 builds 的 AssetBundleBuild 类型的列表，用于存储要构建的资源包的信息。
        List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
        //查找拿到文件夹下的目录
        var assets= AssetDatabase.FindAssets("", new[] { "Assets/Addresable_Res" });
        //循环遍历这些东西
        foreach (var a in assets)
        {
            //根据查找到的目录获取得到的路径
            var path = AssetDatabase.GUIDToAssetPath(a);
            //根据路径加载这些物体
            var obj = AssetDatabase.LoadAssetAtPath<Object>(path);

            AssetBundleBuild build = new AssetBundleBuild();

            build.assetBundleName = obj.name;
            build.assetNames = new[] { path };
            builds.Add(build);

        }
        // ============================shaders

        //var shaders = AssetDatabase.FindAssets("t:Shader", new[] { "Packages/io.jagat.artlogic/Shaders/Avatar3D_New" });
        //AssetBundleBuild shaderBuild = new AssetBundleBuild();

        //List<string> paths = new List<string>();
        //foreach (var sh in shaders)
        //{
        //    var path = AssetDatabase.GUIDToAssetPath(sh);
        //    paths.Add(path);
           
          
        //}
        //shaderBuild.assetBundleName = "avatar_shaders";
        //shaderBuild.assetNames = paths.ToArray();
        //builds.Add(shaderBuild);


        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath ,builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }
}
